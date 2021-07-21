using KlingerStore.Catalog.Domain.Events;
using KlingerStore.Catalog.Domain.Interfaces;
using KlingerStore.Catalog.Domain.Interfaces.Services;
using KlingerStore.Core.Domain.Communication.Mediatr;
using System;
using System.Threading.Tasks;

namespace KlingerStore.Catalog.Domain.Service
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatrHandler _bus;
        public StockService(IProductRepository productRepository, IMediatrHandler bus)
        {
            _productRepository = productRepository;
            _bus = bus;
        }
        public async Task<bool> DebitStock(Guid productId, int quantity)
        {
            var product = await _productRepository.FindById(productId);
            if (product is null) return false;
            if (!product.HasStock(quantity)) return false;

            product.DebitStock(quantity);
            await _productRepository.Update(product);

            if (product.QuantityStock < 10)
            {
                await _bus.PublishEvent(new ProductUnderStockEvent(product.Id, product.QuantityStock));
            }
            
            return await _productRepository.UnitOfWork.Commit();
        }
        public async Task<bool> ReplenishStock(Guid productId, int quantity)
        {
            var product = await _productRepository.FindById(productId);
            if (product is null) return false;

            product.ReplenishStock(quantity);
            await _productRepository.Update(product);            
            return await _productRepository.UnitOfWork.Commit();
        }
        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
