using AutoMapper;
using Points.Api.ViewModels.Saldo;
using Points.Domain.Core.Notifications;
using Points.Domain.Saldo.Repository;
using Points.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Points.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaldoController : BaseController
    {
        private const string MENSAGEM_DE_ERRO_SALDO = "O usuário informado não possui saldo";

        readonly IMapper _mapper;
        private readonly ISaldoQueryRepository _saldoQueryRepository;
        public SaldoController(
            IMapper mapper,
            ISaldoQueryRepository saldoQueryRepository,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler bus) : base(notifications, bus)
        {
            _mapper = mapper;
            _saldoQueryRepository = saldoQueryRepository;
        }

        [HttpGet("saldo/{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var saldo = _saldoQueryRepository.ObterPorEmail(email);
            if (saldo == null)
            {
                ModelState.AddModelError("Saldo", MENSAGEM_DE_ERRO_SALDO);
                NotifyModelStateErrors();
                return Response();
            }
            return Ok(_mapper.Map<SaldoResponseViewModel>(saldo));
        }
    }
}