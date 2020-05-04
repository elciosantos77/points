using System.Collections.Generic;
using AutoMapper;
using Points.Api.ViewModels.Extrato;
using Points.Domain.Core.Notifications;
using Points.Domain.Extrato.Commands;
using Points.Domain.Extrato.Repository;
using Points.Domain.Interfaces;
using Points.Domain.Saldo.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Points.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtratoController : BaseController
    {
        readonly IMapper _mapper;
        private readonly IExtratoQueryRepository _extratoQueryRepository;
        public ExtratoController(
            IMapper mapper,
            IExtratoQueryRepository extratoQueryRepository,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler bus) : base(notifications, bus)
        {
            _mapper = mapper;
            _extratoQueryRepository = extratoQueryRepository;
        }

        [HttpPost("cadastrar-compra")]
        public IActionResult Post([FromBody] ExtratoViewModel compra)
        {
            var command = _mapper.Map<CadastrarExtratoCommand>(compra);
            _bus.EnviarComando(command);

            var extratoDomain = _extratoQueryRepository.ObterUltimaCompra(compra.Email);
            return Response(_mapper.Map<ExtratoViewModel>(extratoDomain));
        }

        [HttpGet("extrato/{email}")]
        public ActionResult<List<ExtratoViewModel>> Get(string email)
        {
            return Ok(_mapper.Map<IEnumerable<ExtratoViewModel>>(_extratoQueryRepository.ObterPorEmail(email).Result));
        }
    }
}