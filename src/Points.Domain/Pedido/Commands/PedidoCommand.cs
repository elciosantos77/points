using Points.Domain.Core.Commands;

namespace Points.Domain.Pedido.Commands
{
    public abstract class PedidoCommand : Command
    {
        public Pedido Pedido { get; internal set; }
    }


}
