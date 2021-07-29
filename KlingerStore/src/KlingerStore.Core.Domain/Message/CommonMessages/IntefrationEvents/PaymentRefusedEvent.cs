using System;

namespace KlingerStore.Core.Domain.Message.CommonMessages.IntefrationEvents
{
    public class PaymentRefusedEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public Guid PaymentId { get; set; }
        public Guid TransactionId { get; set; }
        public decimal TotalValue { get; set; }

        public PaymentRefusedEvent(Guid orderId, Guid clientId, Guid paymentId, Guid transactionId, decimal totalValue)
        {
            OrderId = orderId;
            ClientId = clientId;
            PaymentId = paymentId;
            TransactionId = transactionId;
            TotalValue = totalValue;
        }
    }
}
