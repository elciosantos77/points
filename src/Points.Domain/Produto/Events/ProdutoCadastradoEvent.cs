using Points.Domain.Core.Models;

namespace Points.Domain.Produto.Events
{
    public class ProdutoCadastradoEvent : Event
    {
        public Produto Element { get; set; }

        public ProdutoCadastradoEvent(Produto element)
        {
            Element = element;
            AggregateId = element.Id;
        }
    }
}
