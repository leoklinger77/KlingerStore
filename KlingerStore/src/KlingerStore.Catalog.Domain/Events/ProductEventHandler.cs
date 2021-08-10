using KlingerStore.Catalog.Domain.Interfaces;
using KlingerStore.Catalog.Domain.Interfaces.Services;
using KlingerStore.Core.Domain.Communication.Mediatr;
using KlingerStore.Core.Domain.Message.CommonMessages.IntefrationEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KlingerStore.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<OrderDraftOrderInitEvent>,
                                       INotificationHandler<StartOrderEvent>,
                                       INotificationHandler<OrderProcessCanceledEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMediatrHandler _mediatrHandler;

        public ProductEventHandler(IProductRepository productRepository, IStockService stockService, IMediatrHandler mediatrHandler)
        {
            _productRepository = productRepository;
            _stockService = stockService;
            _mediatrHandler = mediatrHandler;
        }

        public Task Handle(OrderDraftOrderInitEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        public async Task Handle(StartOrderEvent message, CancellationToken cancellationToken)
        {
            var result = await _stockService.DebitStock(message.ProductOrder);
            if (result)
            {
                await _mediatrHandler.PublishEvent(new OrderStockConfirmadEvent(message.OrderId, message.ClientId, message.Total, message.ProductOrder,
                                                                                message.NameCart, message.NumberCart, message.ExpiracaoCart, message.CvvCart));
            }
            else
            {
                await _mediatrHandler.PublishEvent(new OrderStockRejectedEvent(message.OrderId, message.ClientId));
            }
        }
        public async Task Handle(OrderProcessCanceledEvent message, CancellationToken cancellationToken)
        {
            await _stockService.ReplenishStock(message.ListProduct);
        }
    }
}
