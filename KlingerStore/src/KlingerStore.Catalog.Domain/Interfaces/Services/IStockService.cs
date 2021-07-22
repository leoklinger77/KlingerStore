using KlingerStore.Core.Domain.DomainObjects.DTOs;
using System;
using System.Threading.Tasks;

namespace KlingerStore.Catalog.Domain.Interfaces.Services
{
    public interface IStockService : IDisposable
    {
        Task<bool> DebitStock(Guid productId, int quantity);
        Task<bool> DebitStock(ListProductOrder listProductOrder);
        Task<bool> ReplenishStock(Guid productId, int quantity);
        Task<bool> ReplenishStock(ListProductOrder listProductOrder);
    }
}
