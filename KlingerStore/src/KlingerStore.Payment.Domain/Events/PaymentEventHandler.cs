using KlingerStore.Core.Domain.DomainObjects.DTOs;
using KlingerStore.Core.Domain.Message.CommonMessages.IntefrationEvents;
using KlingerStore.Payment.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KlingerStore.Payment.Domain.Events
{
    public class PaymentEventHandler : INotificationHandler<OrderStockConfirmadEvent>
    {
        private readonly IPaymentService _paymentService;

        public PaymentEventHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task Handle(OrderStockConfirmadEvent message, CancellationToken cancellationToken)
        {
            var payment = new PaymentOrder
            {
                OrderId = message.OrderId,
                ClientId = message.ClientId, 
                Total = message.Total,
                NameCart = message.NameCart,
                NumberCart = message.NumberCart,
                ExpiracaoCart = message.ExpiracaoCart,
                CvvCart = message.CvvCart
            };

            await _paymentService.MakePaymentOnRequest(payment);
        }
    }
}
