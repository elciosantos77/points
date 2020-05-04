using Points.Domain.Core.Models;

namespace Points.Domain.Interfaces
{
    public interface IEventStore
    {
        void SalvarEvento<T>(T evento) where T : Event;
    }
}
