using Points.Domain.Core.EventSourcing;
using Points.Domain.Core.Models;
using Points.Domain.Interfaces;

namespace Points.Infra.Data.NoSQL.EventSourcing
{
    public class EventStore : IEventStore
    {
        readonly IEventStoreRepository _repository;

        public EventStore(IEventStoreRepository repository)
        {
            _repository = repository;
        }

        public void SalvarEvento<T>(T evento) where T : Event
        {
            var storedEvent = new StoredEvent(evento, "");
            _repository.Store(storedEvent);
        }
    }
}
