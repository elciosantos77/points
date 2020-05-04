using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Points.Domain.Usuario.Events
{
    public class UsuarioEventHandler :
       INotificationHandler<UsuarioRegistradoEvent>
    {
        public Task Handle(UsuarioRegistradoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
