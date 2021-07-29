using KlingerStore.Core.Domain.Communication.Mediatr;
using KlingerStore.Core.Domain.DomainObjects.DTOs;
using KlingerStore.Core.Domain.Message.CommonMessages.IntefrationEvents;
using KlingerStore.Core.Domain.Message.CommonMessages.Notification;
using KlingerStore.Payment.Domain.Class;
using KlingerStore.Payment.Domain.Class.Enum;
using KlingerStore.Payment.Domain.Interfaces;
using System.Threading.Tasks;

namespace KlingerStore.Payment.Data.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentCartCreditFacade _paymentCartCreditFacade;
        private readonly IPaymentRepository _paymentRepositry;
        private readonly IMediatrHandler _mediatrHandler;

        public PaymentService(IPaymentCartCreditFacade paymentCartCreditFacade, IPaymentRepository paymentRepositry, IMediatrHandler mediatrHandler)
        {
            _paymentCartCreditFacade = paymentCartCreditFacade;
            _paymentRepositry = paymentRepositry;
            _mediatrHandler = mediatrHandler;
        }

        public async Task<Transaction> MakePaymentOnRequest(PaymentOrder paymententOrder)
        {
            var order = new Order
            {
                OrderId = paymententOrder.OrderId,
                Value = paymententOrder.Total
            };

            var payment = new Domain.Class.Payment
            {
                Value = paymententOrder.Total,
                NameCart = paymententOrder.NameCart,
                NumberCart = paymententOrder.NumberCart,
                ExpiracaoCart = paymententOrder.ExpiracaoCart,
                CvvCart = paymententOrder.CvvCart,
                OrderId = paymententOrder.OrderId
            };

            var transacao = await _paymentCartCreditFacade.MakePayment(order, payment);

            if (transacao.StatusTransaction == StatusTransaction.Pago)
            {
                payment.AddEvent(new PaymentSuccessEvent(order.OrderId, paymententOrder.ClientId, transacao.PaymentId, transacao.OrderId, order.Value));

                await _paymentRepositry.Insert(payment);
                await _paymentRepositry.InsertTransaction(transacao);

                await _paymentRepositry.UnitOfWork.Commit();
                return transacao;
            }

            await _mediatrHandler.PublishNotification(new DomainNotification("pagamento", "A operadora recusou o pagamento"));
            await _mediatrHandler.PublishEvent(new PaymentRefusedEvent(order.OrderId, paymententOrder.ClientId, transacao.PaymentId, transacao.OrderId, order.Value));

            return transacao;
        }
    }
}
