using KlingerStore.Core.Domain.Communication.Mediatr;
using KlingerStore.Core.Domain.Message;
using KlingerStore.Core.Domain.Message.CommonMessages.Notification;
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
            }
            else
            {
                order.AddItem(orderItem);

                if (order.OrderItemExists(orderItem))
                {
                    _orderRepository.Insert(order.OrderItems.FirstOrDefault(x => x.ProductId == orderItem.ProductId));
                }
                else
                {
                    _orderRepository.Insert(orderItem);
                }
            }

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
