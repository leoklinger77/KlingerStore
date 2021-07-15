using System;

namespace KlingerStore.Core.Domain.Message
{
    public abstract class Message
    {
        public string MessageType { get; set; }
        public Guid AggregateId { get; set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
