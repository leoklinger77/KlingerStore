using System;

namespace KlingerStore.Core.Domain.Message.CommonMessages.IntefrationEvents
{
    public class FinishOrderEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }

        public FinishOrderEvent(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
