using KlingerStore.Core.Domain.Message;
using System;

namespace KlingerStore.Sales.Application.Commands
{
    public class CanceledProcessOrderCommand : Command
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }

        public CanceledProcessOrderCommand(Guid orderId, Guid clientId)
        {
            OrderId = orderId;
            ClientId = clientId;
        }
    }
}
