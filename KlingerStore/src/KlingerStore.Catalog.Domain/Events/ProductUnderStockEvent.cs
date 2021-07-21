using KlingerStore.Core.Domain.Message.CommonMessages.DomainEvents;
using System;

namespace KlingerStore.Catalog.Domain.Events
{
    public class OrderDraftOrderInitEvent : DomainEvent
    {
        public int QuantityRemaining { get; set; }
        public OrderDraftOrderInitEvent(Guid aggregateId, int quantityRemaining) : base(aggregateId)
        {
            QuantityRemaining = quantityRemaining;
        }
    }
}
