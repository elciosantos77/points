using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Points.Api.ViewModels;
using Points.Domain.Core.Notifications;
using Points.Domain.Endereco.Commands;
using Points.Domain.Endereco.Repository;
using Points.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Points.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecosController : BaseController
    {
        private const string MENSAGEM_DE_ERRO_ENDERECO = "O usuário informado não possui endereço cadastrado";

        readonly IMapper _mapper;
        private readonly IEnderecoQueryRepository _enderecoQueryRepository;
        public EnderecosController(
            IMapper mapper,
            IEnderecoQueryRepository enderecoQueryRepository,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler bus) : base(notifications, bus)
        {
            _mapper = mapper;
            _enderecoQueryRepository = enderecoQueryRepository;
        }

        [HttpPost("cadastrar")]
        public IActionResult Post([FromBody] EnderecoViewModel endereco)
        {
            var command = _mapper.Map<RegistrarEnderecoCommand>(endereco);
            _bus.EnviarComando(command);

            var enderecoDomain = _enderecoQueryRepository.ObterPorEmail(endereco.Email);
            return Response(_mapper.Map<EnderecoViewModel>(enderecoDomain));
        }

        [HttpGet("listar/{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var endereco = _enderecoQueryRepository.ObterPorEmail(email);
            if (endereco == null)
            {
                ModelState.AddModelError("Endereco", MENSAGEM_DE_ERRO_ENDERECO);
                NotifyModelStateErrors();
                return Response();
            }
            return Ok(_mapper.Map<EnderecoViewModel>(endereco));
        }
    }
}