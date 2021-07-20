using KlingerStore.Core.Domain.Message;
using System;

namespace KlingerStore.Core.Domain.DomainObjects
{
    public class DomainEvent : Event
    {
        public DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
