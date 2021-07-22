using KlingerStore.Core.Domain.Message;
using System;

namespace KlingerStore.Sales.Application.Events
{
    public class OrderProductRemoveEvent : Event
    {
        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProdutId { get; private set; }

        public OrderProductRemoveEvent(Guid clientId, Guid orderId, Guid produtId)
        {
            AggregateId = orderId;

            ClientId = clientId;
            OrderId = orderId;
            ProdutId = produtId;
        }
    }
}
