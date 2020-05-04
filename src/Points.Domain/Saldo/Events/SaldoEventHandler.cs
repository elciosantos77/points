using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Points.Domain.Saldo.Events
{
    public class SaldoEventHandler :
       INotificationHandler<SaldoAtualizadoEvent>
    {
        public Task Handle(SaldoAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
