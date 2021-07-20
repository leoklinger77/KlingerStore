using KlingerStore.Core.Domain.Data.Interfaces;
using KlingerStore.Sales.Domain.Class;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KlingerStore.Sales.Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> FindById(Guid id);
        Task<IEnumerable<Order>> FindAllPerClientId(Guid id);
        Task<Order> GetDraftOrderPerCustomer(Guid id);
        void Insert(Order order);
        void Update(Order order);

        Task<OrderItem> FindOrderItemId(Guid id);
        Task<OrderItem> FindOrderItemPerOrder(Guid orderId, Guid productId);
        void Insert(OrderItem orderItem);
        void Update(OrderItem orderItem);
        void Remove(OrderItem orderItem);

        Task<Voucher> FindVoucherPerCode(string code);

    }
}
