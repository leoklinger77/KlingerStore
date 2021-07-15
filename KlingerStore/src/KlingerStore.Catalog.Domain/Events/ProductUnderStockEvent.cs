using KlingerStore.Core.Domain.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
