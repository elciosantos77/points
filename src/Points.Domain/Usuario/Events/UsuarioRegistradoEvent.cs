using Points.Domain.Core.Models;
using Points.Infra.CrossCutting.Identity;

namespace Points.Domain.Usuario.Events
{
    public class UsuarioRegistradoEvent : Event
    {
        public ApplicationUser Element { get; set; }

        public UsuarioRegistradoEvent(ApplicationUser element)
        {
            Element = element;
            AggregateId = element.Id;
        }
    }
}
