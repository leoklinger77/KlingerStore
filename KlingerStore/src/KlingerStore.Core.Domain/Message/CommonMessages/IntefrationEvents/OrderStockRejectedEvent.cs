using System;

namespace KlingerStore.Core.Domain.Message.CommonMessages.IntefrationEvents
{
    public class OrderStockRejectedEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }

        public OrderStockRejectedEvent(Guid orderId, Guid clientId)
        {
            OrderId = orderId;
            ClientId = clientId;
        }
    }
}
