using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Points.Domain.Extrato.Events
{
    public class ExtratoEventHandler :
       INotificationHandler<ExtratoCadastradoEvent>
    {
        public Task Handle(ExtratoCadastradoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
