using Points.Domain.Core.Models;

namespace Points.Domain.Saldo.Events
{
    public class SaldoAtualizadoEvent : Event
    {
        public Saldo Element { get; set; }

        public SaldoAtualizadoEvent(Saldo element)
        {
            Element = element;
            AggregateId = element.Id;
        }
    }
}
