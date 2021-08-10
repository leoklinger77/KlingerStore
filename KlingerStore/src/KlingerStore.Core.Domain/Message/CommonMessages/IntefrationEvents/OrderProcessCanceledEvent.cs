using KlingerStore.Core.Domain.DomainObjects.DTOs;
using System;

namespace KlingerStore.Core.Domain.Message.CommonMessages.IntefrationEvents
{
    public class OrderProcessCanceledEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public ListProductOrder ListProduct { get; set; }

        public OrderProcessCanceledEvent(Guid orderId, Guid clientId, ListProductOrder listProduct)
        {
            AggregateId = orderId;

            OrderId = orderId;
            ClientId = clientId;
            ListProduct = listProduct;
        }
    }
}
