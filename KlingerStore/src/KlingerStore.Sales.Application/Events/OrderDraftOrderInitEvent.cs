using KlingerStore.Core.Domain.Message;
using System;

namespace KlingerStore.Sales.Application.Events
{
    public class OrderDraftOrderInitEvent : Event
    {
        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }

        public OrderDraftOrderInitEvent(Guid clientId, Guid orderId)
        {
            AggregateId = orderId;
            ClientId = clientId;
            OrderId = orderId;
        }
    }
}
