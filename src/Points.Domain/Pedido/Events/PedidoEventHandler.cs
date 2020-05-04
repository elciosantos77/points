using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Points.Domain.Pedido.Events
{
    public class PedidoEventHandler :
       INotificationHandler<PedidoRealizadoEvent>
    {
        public Task Handle(PedidoRealizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
