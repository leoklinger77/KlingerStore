using KlingerStore.Catalog.Domain.Events;
using KlingerStore.Catalog.Domain.Interfaces;
using KlingerStore.Catalog.Domain.Interfaces.Services;
using KlingerStore.Core.Domain.Communication.Mediatr;
using KlingerStore.Core.Domain.DomainObjects.DTOs;
using System;
using System.Threading.Tasks;

namespace KlingerStore.Catalog.Domain.Service
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatrHandler _mediatrHandler;
        public StockService(IProductRepository productRepository, IMediatrHandler mediatrHandler)
        {
            _productRepository = productRepository;
            _mediatrHandler = mediatrHandler;
        }
        public async Task<bool> DebitStock(Guid productId, int quantity)
        {
            if (await DebitItemStock(productId, quantity)) return false;

            return await _productRepository.UnitOfWork.Commit();
        }
        public async Task<bool> DebitStock(ListProductOrder listProductOrder)
        {
            foreach (var item in listProductOrder.Items)
            {
                if (await DebitItemStock(item.Id, item.Quantity)) return false;
            }
            return await _productRepository.UnitOfWork.Commit();
        }        
        public async Task<bool> ReplenishStock(Guid productId, int quantity)
        {
            if(!await ReplenishItemStock(productId, quantity)) return false; ;
            return await _productRepository.UnitOfWork.Commit();
        }
        public async Task<bool> ReplenishStock(ListProductOrder listProductOrder)
        {
            foreach (var item in listProductOrder.Items)
            {
                await ReplenishItemStock(item.Id, item.Quantity);
            }
            return await _productRepository.UnitOfWork.Commit();
        }
        public void Dispose()
        {
            _productRepository?.Dispose();
        }
        private async Task<bool> DebitItemStock(Guid productId, int quantity)
        {
            var product = await _productRepository.FindById(productId);
            if (product is null) return false;
            if (!product.HasStock(quantity)) return false;

            product.DebitStock(quantity);

            if (product.QuantityStock < 10)
            {
                await _mediatrHandler.PublishEvent(new OrderDraftOrderInitEvent(product.Id, product.QuantityStock));
            }

            await _productRepository.Update(product);
            return true;
        }
        private async Task<bool> ReplenishItemStock(Guid productId, int quantity)
        {
            var product = await _productRepository.FindById(productId);
            if (product is null) return false;

            product.ReplenishStock(quantity);
            await _productRepository.Update(product);
            return true;
        }
        
    }
}
