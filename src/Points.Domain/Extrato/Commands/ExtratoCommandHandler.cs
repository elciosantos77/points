using Points.Domain.Core.Notifications;
using Points.Domain.Extrato.Events;
using Points.Domain.Extrato.Repository;
using Points.Domain.Handlers;
using Points.Domain.Interfaces;
using Points.Domain.Saldo.Events;
using Points.Domain.Saldo.Repository;
using Points.Domain.Usuario.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Points.Domain.Extrato.Commands
{
    public class ExtratoCommandHandler : CommandHandler, INotificationHandler<CadastrarExtratoCommand>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IExtratoQueryRepository _extratoQueryRepository;
        private readonly IExtratoCommandRepository _extratoCommandRepository;
        private readonly IUsuarioQueryRepository _usuarioQueryRepository;
        private readonly ISaldoQueryRepository _saldoQueryRepository;
        private readonly ISaldoCommandRepository _saldoCommandRepository;

        public ExtratoCommandHandler(INotificationHandler<DomainNotification> notifications,
                                     IMediatorHandler mediator,
                                     IExtratoQueryRepository extratoQueryRepository,
                                     IExtratoCommandRepository extratoCommandRepository,
                                     IUsuarioQueryRepository usuarioQueryRepository,
                                     ISaldoQueryRepository saldoQueryRepository,
                                     ISaldoCommandRepository saldoCommandRepository)
            : base(mediator, notifications)

        {
            _mediator = mediator;
            _extratoQueryRepository = extratoQueryRepository;
            _extratoCommandRepository = extratoCommandRepository;
            _usuarioQueryRepository = usuarioQueryRepository;
            _saldoQueryRepository = saldoQueryRepository;
            _saldoCommandRepository = saldoCommandRepository;
        }

        public Task Handle(CadastrarExtratoCommand notification, CancellationToken cancellationToken)
        {
            if (!_usuarioQueryRepository.UsuarioExistente(notification.Email))
            {
                NotificarErro(notification.MessageType, $"O usuario {notification.Email} não está cadastrado");
                return Task.CompletedTask;
            } 

            var extrato = new Extrato(notification.Pontos, notification.Email, notification.Estabelecimento, DateTime.Now);
            _extratoCommandRepository.Adicionar(extrato);

            if (!HasNotificationsError())
                _mediator.PublicarEvento(new ExtratoCadastradoEvent(extrato));

            var saldo = AtualizarSaldo(notification.Email, notification.Pontos, notification.MessageType);

            if (!HasNotificationsError())
                _mediator.PublicarEvento(new SaldoAtualizadoEvent(saldo));

            return Task.CompletedTask;
        }

        public Saldo.Saldo AtualizarSaldo(string email, int pontos, string messageType)
        {
            var saldo = _saldoQueryRepository.ObterPorEmail(email);

            try
            {
                if (saldo != null)
                {
                    saldo.Pontos = saldo.Pontos + pontos;
                    _saldoCommandRepository.Atualizar(saldo);
                }
                else
                {
                    saldo = new Saldo.Saldo(email, pontos);
                    _saldoCommandRepository.Cadastrar(saldo);
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
