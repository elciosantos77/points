using System.Threading.Tasks;
using AutoMapper;
using Points.Api.ViewModels;
using Points.Domain.Core.Notifications;
using Points.Domain.Interfaces;
using Points.Domain.Interfaces.Services;
using Points.Domain.Usuario.Commands;
using Points.Infra.CrossCutting.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Points.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : BaseController
    {
        private const string MENSAGEM_DE_ERRO_LOGIN = "Ocorreu um erro ao fazer login, verifique seu nome de usuário e senha.";

        readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public UsuariosController(IMediatorHandler bus,
                                  IMapper mapper,
                                  IUsuarioService usuarioService,
                                  SignInManager<ApplicationUser> signInManager,
                                  UserManager<ApplicationUser> userManager,
        INotificationHandler<DomainNotification> notifications) : base(notifications, bus)
        {
            _mapper = mapper;
            _usuarioService = usuarioService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("cadastrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioRequestViewModel viewmodel)
        {
            RegistrarUsuarioCommand command = _mapper.Map<RegistrarUsuarioCommand>(viewmodel);
            await _bus.EnviarComando(command);

            ApplicationUser usuario = await _userManager.FindByEmailAsync(viewmodel.Email);
            return Response(_mapper.Map<UsuarioResponseViewModel>(usuario));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel viewmodel)
        {
            var result = await _signInManager.PasswordSignInAsync(viewmodel.Email, viewmodel.Senha, false, true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Login", MENSAGEM_DE_ERRO_LOGIN);
                NotifyModelStateErrors();
                return Response();
            }

            ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(viewmodel);
            return Response(_usuarioService.Autenticar(applicationUser).Result);
        }
    }
}