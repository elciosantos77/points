using Points.Domain.Core.Models;

namespace Points.Domain.Pedido.Events
{
    public class PedidoRealizadoEvent : Event
    {
        public Pedido Element { get; set; }

        public PedidoRealizadoEvent(Pedido element)
        {
            Element = element;
            AggregateId = element.Id;
        }
    }
}
