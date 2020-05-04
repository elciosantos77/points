using Points.Domain.Core.Models;

namespace Points.Domain.Endereco
{
    public class EnderecoRegistradoEvent : Event
    {
        public Endereco Element { get; set; }

        public EnderecoRegistradoEvent(Endereco element)
        {
            Element = element;
            AggregateId = element.Id;
        }
    }
}
