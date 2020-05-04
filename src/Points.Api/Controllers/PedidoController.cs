using System.Collections.Generic;
using AutoMapper;
using Points.Api.ViewModels.Pedido;
using Points.Domain.Core.Notifications;
using Points.Domain.Interfaces;
using Points.Domain.Pedido.Commands;
using Points.Domain.Pedido.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Points.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : BaseController
    {
        readonly IMapper _mapper;
        private readonly IPedidoQueryRepository _pedidoQueryRepository;
        public PedidoController(
            IMapper mapper,
            IPedidoQueryRepository pedidoQueryRepository,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler bus) : base(notifications, bus)
        {
            _mapper = mapper;
            _pedidoQueryRepository = pedidoQueryRepository;
        }

        [HttpPost("resgate-produtos")]
        public IActionResult Post([FromBody] PedidoRequestViewModel pedido)
        {
            var command = _mapper.Map<RealizarPedidoCommand>(pedido);
            _bus.EnviarComando(command);

            var pedidoDomain = _pedidoQueryRepository.ObterUltimoPedido(pedido.Email);
            return Response(_mapper.Map<PedidoResponseViewModel>(pedidoDomain));
        }

        [HttpGet("listar/{email}")]
        public ActionResult<List<PedidoRequestViewModel>> Get(string email)
        {
            return Ok(_mapper.Map<IEnumerable<PedidoResponseViewModel>>(_pedidoQueryRepository.ObterPorEmail(email).Result));
        }
    }
}