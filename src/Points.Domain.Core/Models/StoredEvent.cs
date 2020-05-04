using System;

namespace Points.Domain.Core.Models
{
    public class StoredEvent : Event
    {
        protected StoredEvent() { }

        public StoredEvent(Event evento, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = evento.AggregateId;
            MessageType = evento.MessageType;
            Data = evento;
            User = user;
        }

        public Guid Id { get; private set; }

        public Message Data { get; private set; }

        public string User { get; private set; }
    }
}
