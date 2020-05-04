using Points.Domain.Core.Notifications;
using Points.Domain.Handlers;
using Points.Domain.Interfaces;
using Points.Domain.Saldo.Events;
using Points.Domain.Saldo.Repository;
using Points.Domain.Usuario.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Points.Domain.Saldo.Commands
{
    public class SaldoCommandHandler : CommandHandler, INotificationHandler<AtualizarSaldoCommand>
    {
        private readonly IMediatorHandler _mediator;
        private readonly ISaldoQueryRepository _saldoQueryRepository;
        private readonly ISaldoCommandRepository _saldoCommandRepository;
        private readonly IUsuarioQueryRepository _usuarioQueryRepository;

        public SaldoCommandHandler(INotificationHandler<DomainNotification> notifications,
                                     IMediatorHandler mediator,
                                     ISaldoQueryRepository saldoQueryRepository,
                                     ISaldoCommandRepository saldoCommandRepository,
                                     IUsuarioQueryRepository usuarioQueryRepository)
            : base(mediator, notifications)

        {
            _mediator = mediator;
            _saldoQueryRepository = saldoQueryRepository;
            _saldoCommandRepository = saldoCommandRepository;
            _usuarioQueryRepository = usuarioQueryRepository;
        }

        public Task Handle(AtualizarSaldoCommand notification, CancellationToken cancellationToken)
        {
            if (!_usuarioQueryRepository.UsuarioExistente(notification.Email))
            {
                NotificarErro(notification.MessageType, $"O usuario {notification.Email} não está cadastrado");
                return Task.CompletedTask;
            }

            var saldo = new Saldo(notification.Email, notification.Pontos);
            _saldoCommandRepository.Atualizar(saldo);

            if (!HasNotificationsError())
                _mediator.PublicarEvento(new SaldoAtualizadoEvent(saldo));

            return Task.CompletedTask;
        }
    }
}
