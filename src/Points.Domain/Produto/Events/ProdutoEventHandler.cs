using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Points.Domain.Produto.Events
{
    public class ProdutoEventHandler :
       INotificationHandler<ProdutoCadastradoEvent>
    {
        public Task Handle(ProdutoCadastradoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
