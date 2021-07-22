using KlingerStore.Core.Domain.Communication.Mediatr;
using KlingerStore.Core.Domain.DomainObjects.DTOs;
using KlingerStore.Core.Domain.Extensions;
using KlingerStore.Core.Domain.Message;
using KlingerStore.Core.Domain.Message.CommonMessages.IntefrationEvents;
using KlingerStore.Core.Domain.Message.CommonMessages.Notification;
using KlingerStore.Sales.Application.Events;
using KlingerStore.Sales.Domain.Class;
using KlingerStore.Sales.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KlingerStore.Sales.Application.Commands
{
    public class OrderCommandHandler : IRequestHandler<AddOrderItemCommand, bool>,
                                       IRequestHandler<UpdateOrderItemCommand, bool>,
                                       IRequestHandler<RemoverOrderItemCommand, bool>,
                                       IRequestHandler<ApplyVoucherOrderItemCommand, bool>,
                                       IRequestHandler<StartOrderCommand, bool>
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
        public async Task<bool> Handle(UpdateOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderPerCustomer(message.ClientId);

            if (order is null)
            {
                await _mediatrHandler.PublishNotification(new DomainNotification("Pedido", "Pedido não encontrado!"));
                return false;
            }
            var orderItem = await _orderRepository.FindOrderItemPerOrder(order.Id, message.ProductId);
            if (!order.OrderItemExists(orderItem))
            {
                await _mediatrHandler.PublishNotification(new DomainNotification("Pedido", "Item do pedido não encontrado!"));
                return false;
            }

            order.UpdateUnity(orderItem, message.Quantity);

            _orderRepository.Update(orderItem);
            _orderRepository.Update(order);

            order.AddEvent(new OrderItemUpdateEvent(message.ClientId, order.Id, order.TotalValue));
            order.AddEvent(new OrderProductUpdateEvent(message.ClientId, order.Id, message.ProductId, message.Quantity));
            return await _orderRepository.UnitOfWork.Commit();

        }
        public async Task<bool> Handle(RemoverOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderPerCustomer(message.ClientId);

            if (order is null)
            {
                await _mediatrHandler.PublishNotification(new DomainNotification("Pedido", "Pedido não encontrado!"));
                return false;
            }

            var orderItem = await _orderRepository.FindOrderItemPerOrder(order.Id, message.ProductId);
            if (orderItem is null && !order.OrderItemExists(orderItem))
            {
                await _mediatrHandler.PublishNotification(new DomainNotification("Pedido", "Item do pedido não encontrado!"));
                return false;
            }

            order.RemoveItem(orderItem);

            _orderRepository.Remove(orderItem);
            _orderRepository.Update(order);
            order.AddEvent(new OrderItemUpdateEvent(message.ClientId, order.Id, order.TotalValue));
            order.AddEvent(new OrderProductRemoveEvent(message.ClientId, order.Id, message.ProductId));
            return await _orderRepository.UnitOfWork.Commit();

        }
        public async Task<bool> Handle(ApplyVoucherOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderPerCustomer(message.ClientId);

            if (order is null)
            {
                await _mediatrHandler.PublishNotification(new DomainNotification("Pedido", "Pedido não encontrado!"));
                return false;
            }

            var voucher = await _orderRepository.FindVoucherPerCode(message.VoucherCode);

            if (voucher is null)
            {
                await _mediatrHandler.PublishNotification(new DomainNotification("Pedido", "Voucher não encontrado!"));
                return false;
            }

            var applyVoucherValidation = order.ApplyVoucher(voucher);

            if (!applyVoucherValidation.IsValid)
            {
                foreach (var item in applyVoucherValidation.Errors)
                {
                    await _mediatrHandler.PublishNotification(new DomainNotification(item.ErrorCode, item.ErrorMessage));
                }
                return false;
            }

            order.ApplyVoucher(voucher);

            order.AddEvent(new VoucherApplyOrderEvent(message.ClientId, order.Id, voucher.Id));
            order.AddEvent(new OrderItemUpdateEvent(message.ClientId, order.Id, order.TotalValue));

            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();
        }
        public async Task<bool> Handle(StartOrderCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderPerCustomer(message.ClientId);
            order.InityOrder();

            var itensList = new List<Item>();
            order.OrderItems.ForEach(x => itensList.Add(new Item(x.ProductId, x.Quantity)));
            var listProductOrder = new ListProductOrder(order.Id, itensList);

            order.AddEvent(new StartOrderEvent(order.Id, order.ClientId, order.TotalValue, listProductOrder, message.NameCart, message.NumberCart, message.ExpiracaoCart, message.CvvCart));

            _orderRepository.Update(order);
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
