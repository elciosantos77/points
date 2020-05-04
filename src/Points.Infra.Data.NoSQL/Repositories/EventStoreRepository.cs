using Points.Domain.Core.EventSourcing;
using Points.Domain.Core.Models;

namespace Points.Infra.Data.NoSQL.Repositories
{
    public class EventStoreRepository : BaseRepository<StoredEvent>, IEventStoreRepository
    {
        private const string COLLECTION_NAME = "events";

        public EventStoreRepository()
            : base(COLLECTION_NAME) { }

        public void Store(StoredEvent @event)
        {
            collection.InsertOne(@event);
        }
    }
}
