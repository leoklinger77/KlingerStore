using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KlingerStore.Sales.Application.Events
{
    public class OrderEventHandler :
            INotificationHandler<OrderDraftOrderInitEvent>,
            INotificationHandler<OrderItemAddEvent>,
            INotificationHandler<OrderItemUpdateEvent>
    {
        public Task Handle(OrderDraftOrderInitEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderItemAddEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderItemUpdateEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
