using KlingerStore.Catalog.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KlingerStore.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<ProductUnderStockEvent>
    {
        private readonly IProductRepository _productRepository;

        public ProductEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(ProductUnderStockEvent notification, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindById(notification.AggregateId);                        
        }
    }
}
