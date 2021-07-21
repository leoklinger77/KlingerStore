using KlingerStore.Core.Domain.Communication.Mediatr;
using KlingerStore.Core.Domain.Message;
using KlingerStore.Core.Domain.Message.CommonMessages.Notification;
using KlingerStore.Sales.Application.Events;
using KlingerStore.Sales.Domain.Class;
using KlingerStore.Sales.Domain.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KlingerStore.Sales.Application.Commands
{
    public class OrderCommandHandler : IRequestHandler<AddOrderItemCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatrHandler _mediatrHandler;

        public OrderCommandHandler(IOrderRepository orderRepository, IMediatrHandler mediatrHandler)
        {
            _orderRepository = orderRepository;
            _mediatrHandler = mediatrHandler;
        }

        public async Task<bool> Handle(AddOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderPerCustomer(message.ClientId);
            var orderItem = new OrderItem(message.ProductId, message.Name, message.Quantity, message.UnitValue);

            if (order is null)
            {
                order = Order.OrderFactory.NewOrderDraft(message.ClientId);
                order.AddItem(orderItem);

                _orderRepository.Insert(order);
                order.AddEvent(new OrderDraftOrderInitEvent(message.ClientId, message.ProductId));
            }
            else
            {
                var orderExists = order.OrderItemExists(orderItem);
                order.AddItem(orderItem);

                if (orderExists)
                {
                    _orderRepository.Update(order.OrderItems.FirstOrDefault(x => x.ProductId == orderItem.ProductId));

                }
                else
                {
                    _orderRepository.Insert(orderItem);
                }
                order.AddEvent(new OrderItemUpdateEvent(message.ClientId, order.Id, order.TotalValue));
            }

            order.AddEvent(new OrderItemAddEvent(message.ClientId, order.Id, message.ProductId, message.Name, message.UnitValue, message.Quantity));
            return await _orderRepository.UnitOfWork.Commit();
        }

        private bool ValidateCommand(Command message)
        {
            if (message.IsValid()) return true;

            foreach (var item in message.ValidationResult.Errors)
            {
                _mediatrHandler.PublishNotification(new DomainNotification(message.MessageType, item.ErrorMessage));
            }

            return false;
        }
    }
}
