using KlingerStore.Core.Domain.Message;
using System;

namespace KlingerStore.Sales.Application.Commands
{
    public class FinishOrderCommand : Command
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }

        public FinishOrderCommand(Guid orderId, Guid clientId)
        {
            OrderId = orderId;
            ClientId = clientId;
        }
    }
}
