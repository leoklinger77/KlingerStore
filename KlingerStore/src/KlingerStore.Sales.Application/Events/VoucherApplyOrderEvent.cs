using KlingerStore.Core.Domain.Message;
using System;

namespace KlingerStore.Sales.Application.Events
{
    public class VoucherApplyOrderEvent : Event
    {
        public Guid ClientId { get; set; }
        public Guid OrderId { get; set; }
        public Guid VoucherId { get; set; }

        public VoucherApplyOrderEvent(Guid clientId, Guid orderId, Guid voucherId)
        {
            AggregateId = orderId;

            ClientId = clientId;
            OrderId = orderId;
            VoucherId = voucherId;
        }
    }
}
