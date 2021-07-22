using KlingerStore.Core.Domain.Message;
using System;

namespace KlingerStore.Sales.Application.Events
{
    public class OrderProductUpdateEvent : Event
    {
        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProdutId { get; private set; }
        public int Quantity { get; private set; }
        public OrderProductUpdateEvent(Guid clientId, Guid orderId, Guid produtId, int quantity)
        {
            AggregateId = orderId;

            ClientId = clientId;
            OrderId = orderId;
            ProdutId = produtId;
            Quantity = quantity;
        }
    }
}
