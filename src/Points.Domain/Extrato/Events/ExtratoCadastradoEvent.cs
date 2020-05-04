using Points.Domain.Core.Models;

namespace Points.Domain.Extrato.Events
{
    public class ExtratoCadastradoEvent : Event
    {
        public Extrato Element { get; set; }

        public ExtratoCadastradoEvent(Extrato element)
        {
            Element = element;
            AggregateId = element.Id;
        }
    }
}
