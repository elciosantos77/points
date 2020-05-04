using System.Collections.Generic;

namespace Points.Domain.Pedido.Commands
{
    public class RealizarPedidoCommand : PedidoCommand
    {
        public RealizarPedidoCommand(string email)
        {
            Pedido = new Pedido(email);
        }

        public void AdicionarItem(PedidoItem item)
        {
            var items = Pedido.Itens ?? new List<PedidoItem>();
            items.Add(item);
            Pedido.Itens = items;
        }
    }
}
