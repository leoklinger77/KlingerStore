using KlingerStore.Sales.Application.Querys.ViewModels;
using KlingerStore.Sales.Domain.Class.Enumeration;
using KlingerStore.Sales.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlingerStore.Sales.Application.Querys
{
    public class OrderQuerys : IOrderQuerys
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQuerys(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CartViewModel> FindCartClient(Guid clientId)
        {
            var order = await _orderRepository.GetDraftOrderPerCustomer(clientId);
            if (order is null) return null;

            var cart = new CartViewModel
            {
                ClientId = order.ClientId,
                TotalValue = order.TotalValue,
                OrderId = order.Id,
                ValorDiscount = order.Discount,
                SubTotal = order.Discount + order.TotalValue
            };

            if (order.Voucher != null)
            {
                cart.VoucherCode = order.Voucher.Code;
            }

            foreach (var item in order.OrderItems)
            {
                cart.Items.Add(new CartItemViewModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    ValorUnit = item.UnitValue,
                    TotalValue = item.UnitValue * item.Quantity
                });
            }

            return cart;
        }

        public async Task<IEnumerable<OrderViewModel>> FindOrderClient(Guid clientId)
        {
            var order = await _orderRepository.FindAllPerClientId(clientId);

            order = order.Where(x => x.OrderStatus == OrderStatus.Pago || x.OrderStatus == OrderStatus.Cancelado)
                .OrderByDescending(p => p.Code);

            if (!order.Any()) return null;

            var orderView = new List<OrderViewModel>();

            foreach (var item in order)
            {
                orderView.Add(new OrderViewModel
                {
                    TotalValue = item.TotalValue,
                    OrderStatus = (int)item.OrderStatus,
                    Code = item.Code,
                    InsertDate = item.InsertDate
                });
            }

            return orderView;
        }
    }
}
