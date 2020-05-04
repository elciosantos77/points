using FluentValidation.Results;
using MediatR;
using Points.Domain.Core.Notifications;
using Points.Domain.Interfaces;

namespace Points.Domain.Handlers
{
    public abstract class CommandHandler
    {
        private readonly IMediatorHandler _mediator;
        private readonly DomainNotificationHandler _notifications;

        protected CommandHandler(IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications)
        {
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected void NotificarValidacoesErro(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                NotificarErro(error.PropertyName, error.ErrorMessage);
            }
        }

        protected void NotificarErro(string propertyName, string errorMessage)
        {
            _mediator.PublicarEvento(new DomainNotification(propertyName, errorMessage));
        }

        protected bool HasNotificationsError()
        {
            return _notifications.HasNotifications();
        }
    }
}
