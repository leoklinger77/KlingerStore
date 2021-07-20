using KlingerStore.Core.Domain.DomainObjects;
using System;

namespace KlingerStore.Catalog.Domain.Events
{
    public class ProductUnderStockEvent : DomainEvent
    {
        public int QuantityRemaining { get; set; }
        public ProductUnderStockEvent(Guid aggregateId, int quantityRemaining) : base(aggregateId)
        {
            QuantityRemaining = quantityRemaining;
        }
    }
}
