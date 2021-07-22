using KlingerStore.Core.Domain.Data.Interfaces;
using KlingerStore.Sales.Data.Context;
using KlingerStore.Sales.Domain.Class;
using KlingerStore.Sales.Domain.Class.Enumeration;
using KlingerStore.Sales.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlingerStore.Sales.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SalesContext _context;

        public OrderRepository(SalesContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Order>> FindAllPerClientId(Guid id)
        {
            return await _context.Order.AsNoTracking().Where(x => x.ClientId == id).ToListAsync();
        }

        public async Task<Order> FindById(Guid id)
        {
            return await _context.Order.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<OrderItem> FindOrderItemId(Guid id)
        {
            return await _context.OrderItem.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<OrderItem> FindOrderItemPerOrder(Guid orderId, Guid productId)
        {
            return await _context.OrderItem.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == productId && p.OrderId == orderId);
        }

        public Task<Voucher> FindVoucherPerCode(string code)
        {
            return _context.Voucher.AsNoTracking().Where(x => x.Code == code).FirstOrDefaultAsync();
        }

        public async Task<Order> GetDraftOrderPerCustomer(Guid id)
        {
            var pedido = await _context.Order.FirstOrDefaultAsync(p => p.ClientId == id && p.OrderStatus == OrderStatus.Rascunho);
            if (pedido == null) return null;

            await _context.Entry(pedido)
                .Collection(i => i.OrderItems).LoadAsync();

            if (pedido.VoucherId != null)
            {
                await _context.Entry(pedido)
                    .Reference(i => i.Voucher).LoadAsync();
            }

            return pedido;
        }

        public void Insert(Order order)
        {
            _context.Order.Add(order);
        }

        public void Insert(OrderItem orderItem)
        {
            _context.OrderItem.Add(orderItem);
        }

        public void Remove(OrderItem orderItem)
        {
            _context.OrderItem.Remove(orderItem);
        }

        public void Update(Order order)
        {
            _context.Order.Update(order);
        }

        public void Update(OrderItem orderItem)
        {
            _context.OrderItem.Update(orderItem);
        }

        public void Dispose()
        {
            _context?.DisposeAsync();
        }
    }
}
