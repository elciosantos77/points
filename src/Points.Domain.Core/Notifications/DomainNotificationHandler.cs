﻿using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Points.Domain.Core.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public virtual List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public virtual bool HasNotifications()
        {
            return _notifications.Any();
        }

        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _notifications.Add(notification);
            });
        }

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }
    }
}
