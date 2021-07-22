using KlingerStore.Core.Domain.DomainObjects.DTOs;
using System;

namespace KlingerStore.Core.Domain.Message.CommonMessages.IntefrationEvents
{
    public class OrderStockConfirmadEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public decimal Total { get; private set; }
        public ListProductOrder ProductOrder { get; private set; }
        public string NameCart { get; private set; }
        public string NumberCart { get; private set; }
        public string ExpiracaoCart { get; private set; }
        public string CvvCart { get; private set; }

        public OrderStockConfirmadEvent(Guid orderId, Guid clientId, decimal total, ListProductOrder productOrder, string nameCart, string numberCart, string expiracaoCart, string cvvCart)
        {
            OrderId = orderId;
            ClientId = clientId;
            Total = total;
            ProductOrder = productOrder;
            NameCart = nameCart;
            NumberCart = numberCart;
            ExpiracaoCart = expiracaoCart;
            CvvCart = cvvCart;
        }
    }
}
