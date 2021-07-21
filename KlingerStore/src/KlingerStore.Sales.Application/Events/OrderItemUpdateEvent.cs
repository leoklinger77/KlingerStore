using KlingerStore.Core.Domain.Message;
using System;

namespace KlingerStore.Sales.Application.Events
{
    public class OrderItemUpdateEvent : Event
    {
        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }        
        public decimal TotalValue { get; private set; }

        public OrderItemUpdateEvent(Guid clientId, Guid orderId, decimal totalValue)
        {
            AggregateId = orderId;

            ClientId = clientId;
            OrderId = orderId;            
            TotalValue = totalValue;
        }
    }
}
