using Points.Domain.Core.Notifications;
using Points.Domain.Endereco.Repository;
using Points.Domain.Handlers;
using Points.Domain.Interfaces;
using Points.Domain.Pedido.Events;
using Points.Domain.Pedido.Repository;
using Points.Domain.Produto.Repository;
using Points.Domain.Saldo.Events;
using Points.Domain.Saldo.Repository;
using Points.Domain.Usuario.Repository;
using Points.Domain.Validations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Points.Domain.Pedido.Commands
{
    public class PedidoCommandHandler : CommandHandler, INotificationHandler<RealizarPedidoCommand>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IPedidoQueryRepository _pedidoQueryRepository;
        private readonly IPedidoCommandRepository _pedidoCommandRepository;
        private readonly IProdutoQueryRepository _produtoQueryRepository;
        private readonly IUsuarioQueryRepository _usuarioQueryRepository;
        private readonly ISaldoQueryRepository _saldoQueryRepository;
        private readonly ISaldoCommandRepository _saldoCommandRepository;
        private readonly IEnderecoQueryRepository _enderecoQueryRepository;
        private readonly PedidoValidator _pedidoValidator;

        public PedidoCommandHandler(INotificationHandler<DomainNotification> notifications,
                                     IMediatorHandler mediator,
                                     IPedidoQueryRepository pedidoQueryRepository,
                                     IPedidoCommandRepository pedidoCommandRepository,
                                     IProdutoQueryRepository produtoQueryRespository,
                                     IUsuarioQueryRepository usuarioQueryRepository,
                                     ISaldoQueryRepository saldoQueryRepository,
                                     ISaldoCommandRepository saldoCommandRepository,
                                     IEnderecoQueryRepository enderecoQueryRepository,
                                     PedidoValidator pedidoValidator)
            : base(mediator, notifications)

        {
            _mediator = mediator;
            _pedidoQueryRepository = pedidoQueryRepository;
            _pedidoCommandRepository = pedidoCommandRepository;
            _produtoQueryRepository = produtoQueryRespository;
            _usuarioQueryRepository = usuarioQueryRepository;
            _saldoQueryRepository = saldoQueryRepository;
            _pedidoValidator = pedidoValidator;
            _saldoCommandRepository = saldoCommandRepository;
            _enderecoQueryRepository = enderecoQueryRepository;
        }

        public Task Handle(RealizarPedidoCommand notification, CancellationToken cancellationToken)
        {
            if (!_usuarioQueryRepository.UsuarioExistente(notification.Pedido.Email))
            {
                NotificarErro(notification.MessageType, $"O usuario {notification.Pedido.Email} não está cadastrado");
                return Task.CompletedTask;
            }

            var endereco = PossuiEndereco(notification.Pedido.Email, notification.MessageType);
            if (endereco == null)
                return Task.CompletedTask;

            var pedido = new Pedido(notification.Pedido.Email, Enums.StatusEntrega.AGUARDANDOENVIO, DateTime.Now, new List<PedidoItem>(), endereco);

            foreach (var item in notification.Pedido.Itens)
            {
                item.Produto = _produtoQueryRepository.ObterPorId(item.ProdutoId);

                if (item.Produto == null)
                {
                    NotificarErro(notification.MessageType, $"O id {item.ProdutoId} informado é inválido");
                    return Task.CompletedTask;
                };

                item.CalcularPontuacao(item.Produto.Pontuacao);
                pedido.Itens.Add(item);
            }

            var totalPontosPedido = pedido.CalcularPontuacaoTotal();

            if (!PedidoValido(pedido))
                return Task.CompletedTask;

            if (SaldoInsuficiente(pedido.Email, totalPontosPedido, notification.MessageType))
                return Task.CompletedTask;

            _pedidoCommandRepository.Adicionar(pedido);
            if (!HasNotificationsError())
                _mediator.PublicarEvento(new PedidoRealizadoEvent(pedido));

            var saldo = AtualizarSaldo(pedido.Email, totalPontosPedido, notification.MessageType);
            if (!HasNotificationsError())
                _mediator.PublicarEvento(new SaldoAtualizadoEvent(saldo));

            return Task.CompletedTask;
        }

        private bool PedidoValido(Pedido pedido)
        {
            if (_pedidoValidator.Validar(pedido)) return true;

            NotificarValidacoesErro(_pedidoValidator.ValidationResult);
            return false;
        }

        private bool SaldoInsuficiente(string email, int totalPontosPedido, string messageType)
        {
            var saldoInsuficiente = _saldoQueryRepository.SaldoInsuficiente(totalPontosPedido, email);

            if (saldoInsuficiente)
                NotificarErro(messageType, $"Saldo insuficiente para resgate de produto(s)");

            return saldoInsuficiente;
        }

        private Endereco.Endereco PossuiEndereco(string email, string messageType)
        {
            var endereco = _enderecoQueryRepository.ObterPorEmail(email);

            if (endereco == null)
                NotificarErro(messageType, $"Endereço de entrega não encontrado para o e-mail: {email}");

            return endereco;
        }

        public Saldo.Saldo AtualizarSaldo(string email, int pontos, string messageType)
        {
            var saldo = _saldoQueryRepository.ObterPorEmail(email);

            try
            {
                if (saldo != null)
                {
                    saldo.Pontos = saldo.Pontos - pontos;
                    _saldoCommandRepository.Atualizar(saldo);
                }
                else
                {
                    NotificarErro(messageType, $"Houve um erro ao atualizar o saldo. Saldo não encontrado");
                }
            }
            catch (Exception e)
            {
                NotificarErro(messageType, $"Houve um erro ao atualizar o saldo: {e.Message}");
            }
            return saldo;
        }
    }
}
