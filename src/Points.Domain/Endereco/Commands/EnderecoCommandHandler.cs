using Points.Domain.Core.Notifications;
using Points.Domain.Endereco.Repository;
using Points.Domain.Handlers;
using Points.Domain.Interfaces;
using Points.Domain.Interfaces.Services;
using Points.Domain.Usuario.Repository;
using Points.Domain.Validations;
using Points.Infra.CrossCutting.Identity;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Points.Domain.Endereco.Commands
{
    public class EnderecoCommandHandler : CommandHandler, INotificationHandler<RegistrarEnderecoCommand>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IEnderecoQueryRepository _enderecoQueryRepository;
        private readonly IEnderecoCommandRepository _enderecoCommandRepository;
        private readonly IUsuarioQueryRepository _usuarioQueryRepository;

        public EnderecoCommandHandler(INotificationHandler<DomainNotification> notifications,
                                     IMediatorHandler mediator,
                                     IEnderecoQueryRepository enderecoQueryRepository,
                                     IEnderecoCommandRepository enderecoCommandRepository,
                                     IUsuarioQueryRepository usuarioQueryRepository)
            : base(mediator, notifications)

        {
            _mediator = mediator;
            _enderecoQueryRepository = enderecoQueryRepository;
            _enderecoCommandRepository = enderecoCommandRepository;
            _usuarioQueryRepository = usuarioQueryRepository;
        }

        public Task Handle(RegistrarEnderecoCommand notification, CancellationToken cancellationToken)
        {
            var endereco = new Endereco(notification.Email,
                                        notification.CEP,
                                        notification.Rua,
                                        notification.Numero,
                                        notification.Complemento,
                                        notification.Bairro,
                                        notification.Cidade,
                                        notification.Estado);

            if (!_usuarioQueryRepository.UsuarioExistente(notification.Email))
            {
                NotificarErro(notification.MessageType, $"O usuario {notification.Email} não está cadastrado");
                return Task.CompletedTask;
            }

            if (EnderecoExistente(notification.Email, notification.MessageType) != null)
                return Task.CompletedTask;

            _enderecoCommandRepository.Adicionar(endereco);

            if (!HasNotificationsError())
                _mediator.PublicarEvento(new EnderecoRegistradoEvent(endereco));

            return Task.CompletedTask;
        }

        private Endereco EnderecoExistente(string email, string messageType)
        {
            var endereco = _enderecoQueryRepository.ObterPorEmail(email);

            if (endereco != null)
            {
                NotificarErro(messageType, $"Endereço já cadastrado para o e-mail {email}");
                return endereco;
            }
            return null;
        }
    }
}
