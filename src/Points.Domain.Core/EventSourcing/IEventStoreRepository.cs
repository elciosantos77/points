using Points.Domain.Core.Models;

namespace Points.Domain.Core.EventSourcing
{
    public interface IEventStoreRepository
    {
        void Store(StoredEvent @event);
    }
}
