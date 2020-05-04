using Points.Domain.Core.Notifications;
using Points.Domain.Handlers;
using Points.Domain.Interfaces;
using Points.Domain.Usuario.Events;
using Points.Infra.CrossCutting.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Points.Domain.Usuario.Commands
{

    public class UsuarioCommandHandler : CommandHandler,
                                        INotificationHandler<RegistrarUsuarioCommand>
    {

        private readonly IMediatorHandler _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsuarioCommandHandler(INotificationHandler<DomainNotification> notifications,
                                     IMediatorHandler mediator,
                                     UserManager<ApplicationUser> userManager)
            : base(mediator, notifications)

        {
            _mediator = mediator;
            _userManager = userManager;

        }

        public Task Handle(RegistrarUsuarioCommand notification, CancellationToken cancellationToken)
        {
            var usuario = new ApplicationUser { UserName = notification.Email, Email = notification.Email, Nome = notification.Nome };
            var result = _userManager.CreateAsync(usuario, notification.Senha).Result;

            if (!result.Succeeded)
            {
                NotificarErro(notification.MessageType, $"Usuário já cadastrado com o e-mail: {notification.Email}");
                return Task.CompletedTask;
            }

            if (!HasNotificationsError())
                _mediator.PublicarEvento(new UsuarioRegistradoEvent(usuario));

            return Task.CompletedTask;
        }
    }
}
