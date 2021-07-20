using KlingerStore.Core.Domain.DomainObjects;
using KlingerStore.Core.Domain.Interfaces;
using KlingerStore.Sales.Domain.Class.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KlingerStore.Sales.Domain.Class
{
    public class Order : Entity, IAggregateRoot
    {
        public int Code { get; private set; }
        public Guid ClientId { get; private set; }

        public Voucher Voucher { get; private set; }
        public Guid? VoucherId { get; private set; }

        public bool VoucherUsed { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalValue { get; private set; }
        public DateTime InsertDate { get; private set; }
        public OrderStatus OrderStatus { get; private set; }


        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        protected Order() { _orderItems = new List<OrderItem>(); }
        public Order(Guid clientId, bool voucherUsed, decimal discount, decimal totalValue)
        {
            ClientId = clientId;
            VoucherUsed = voucherUsed;
            Discount = discount;
            TotalValue = totalValue;

            _orderItems = new List<OrderItem>();
        }

        public void ApplyVoucher(Voucher voucher)
        {
            Voucher = voucher;
            VoucherUsed = true;
            CalculateValueOrder();
        }

        public void CalculateValueOrder()
        {
            TotalValue = OrderItems.Sum(x => x.CalculateValue());
            CalulateValueTotalDiscount();
        }

        public void CalulateValueTotalDiscount()
        {
            if (!VoucherUsed) return;

            decimal discount = 0;
            var value = TotalValue;

            if (Voucher.VoucherDiscountType == VoucherDiscountType.Porcentagem)
            {
                if (Voucher.Percentage.HasValue)
                {
                    discount = (value * Voucher.Percentage.Value) / 100;
                    value -= discount;
                }
            }
            else
            {
                if (Voucher.Percentage.HasValue)
                {
                    discount = Voucher.Percentage.Value;
                    value -= discount;
                }
            }

            TotalValue = value < 0 ? 0 : value;
            Discount = discount;
        }

        public bool OrderItemExists(OrderItem orderItem)
        {
            return _orderItems.Any(x => x.ProductId == orderItem.Id);
        }

        public void AddItem(OrderItem item)
        {
            if (!item.IsValid()) return;

            item.AssociateOrder(Id);

            if (OrderItemExists(item))
            {
                var itemExists = _orderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                itemExists.AddUnity(item.Quantity);

                item = itemExists;

                _orderItems.Remove(itemExists);
            }

            item.CalculateValue();
            _orderItems.Add(item);

            CalculateValueOrder();
        }

        public void RemoveItem(OrderItem item)
        {
            if (!item.IsValid()) return;

            var itemExists = OrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);

            if (itemExists is null) throw new DomainException("O item não pertence ao pedido");
            _orderItems.Remove(itemExists);

            CalculateValueOrder();
        }

        public void UpdateItem(OrderItem item)
        {
            if (!item.IsValid()) return;
            item.AssociateOrder(Id);

            var itemExists = OrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);

            if (itemExists is null) throw new DomainException("O Item não pertence ao pedido");

            _orderItems.Remove(itemExists);
            _orderItems.Add(item);

            CalculateValueOrder();
        }

        public void UpdateUnity(OrderItem item, int unity)
        {
            item.UpdateUnity(unity);
            UpdateItem(item);
        }

        public void InityOrder()
        {
            OrderStatus = OrderStatus.Iniciado;
        }

        public void MakeDraft()
        {
            OrderStatus = OrderStatus.Rascunho;
        }

        public void FinishOrder()
        {
            OrderStatus = OrderStatus.Pago;
        }

        public void CanceledOrder()
        {
            OrderStatus = OrderStatus.Cancelado;
        }

        public static class OrderFactory
        {
            public static Order NewOrderDraft(Guid clientId)
            {
                var order = new Order { ClientId = clientId };
                order.MakeDraft();
                return order;
            }
        }
    }
}
