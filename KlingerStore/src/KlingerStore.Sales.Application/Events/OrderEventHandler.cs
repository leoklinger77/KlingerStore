using KlingerStore.Core.Domain.Message.CommonMessages.IntefrationEvents;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KlingerStore.Sales.Application.Events
{
    public class OrderEventHandler :
            INotificationHandler<OrderDraftOrderInitEvent>,
            INotificationHandler<OrderItemAddEvent>,
            INotificationHandler<OrderItemUpdateEvent>,
            INotificationHandler<OrderStockRejectedEvent>
    {        
        public Task Handle(OrderDraftOrderInitEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderItemAddEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderItemUpdateEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderStockRejectedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
