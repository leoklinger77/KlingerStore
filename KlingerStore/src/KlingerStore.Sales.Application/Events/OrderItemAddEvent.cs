using KlingerStore.Core.Domain.Message;
using System;

namespace KlingerStore.Sales.Application.Events
{
    public class OrderItemAddEvent : Event
    {
        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitValue { get; private set; }
        public int Quantity { get; private set; }

        public OrderItemAddEvent(Guid clientId, Guid orderId, Guid productId, string productName, decimal unitValue, int quantity)
        {
            AggregateId = orderId;

            ClientId = clientId;
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            UnitValue = unitValue;
            Quantity = quantity;            
        }
    }
}
