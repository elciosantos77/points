using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Points.Domain.Endereco
{
    public class EnderecoEventHandler :
       INotificationHandler<EnderecoRegistradoEvent>
    {
        public Task Handle(EnderecoRegistradoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
