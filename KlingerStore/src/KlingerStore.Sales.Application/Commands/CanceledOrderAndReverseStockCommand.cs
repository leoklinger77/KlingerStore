using KlingerStore.Core.Domain.Message;
using System;

namespace KlingerStore.Sales.Application.Commands
{
    public class CanceledOrderAndReverseStockCommand : Command
    {
        public Guid OrderId { get; set; }
        public Guid CLientId { get; set; }

        public CanceledOrderAndReverseStockCommand(Guid orderId, Guid cLientId)
        {
            OrderId = orderId;
            CLientId = cLientId;
        }
    }
}
