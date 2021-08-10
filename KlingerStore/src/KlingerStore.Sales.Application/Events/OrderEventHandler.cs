using KlingerStore.Core.Domain.Communication.Mediatr;
using KlingerStore.Core.Domain.Message.CommonMessages.IntefrationEvents;
using KlingerStore.Sales.Application.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KlingerStore.Sales.Application.Events
{
    public class OrderEventHandler :
                                    INotificationHandler<OrderDraftOrderInitEvent>,
                                    INotificationHandler<OrderItemAddEvent>,
                                    INotificationHandler<OrderItemUpdateEvent>,
                                    INotificationHandler<OrderStockRejectedEvent>,
                                    INotificationHandler<PaymentSuccessEvent>,
                                    INotificationHandler<PaymentRefusedEvent>
    {
        private readonly IMediatrHandler _mediatrHandler;

        public OrderEventHandler(IMediatrHandler mediatrHandler)
        {
            _mediatrHandler = mediatrHandler;
        }

        public async Task Handle(OrderDraftOrderInitEvent notification, CancellationToken cancellationToken)
        {
            
        }

        public async Task Handle(OrderItemAddEvent message, CancellationToken cancellationToken)
        {
            
        }

        public async Task Handle(OrderItemUpdateEvent message, CancellationToken cancellationToken)
        {
            
        }

        public async Task Handle(OrderStockRejectedEvent message, CancellationToken cancellationToken)
        {
            await _mediatrHandler.SendCommand(new CanceledProcessOrderCommand(message.OrderId, message.ClientId));
        }

        public async Task Handle(PaymentSuccessEvent message, CancellationToken cancellationToken)
        {
            await _mediatrHandler.SendCommand(new FinishOrderCommand(message.OrderId, message.ClientId));
        }

        public async Task Handle(PaymentRefusedEvent message, CancellationToken cancellationToken)
        {
            await _mediatrHandler.SendCommand(new CanceledOrderAndReverseStockCommand(message.OrderId, message.ClientId));
        }
    }
}
