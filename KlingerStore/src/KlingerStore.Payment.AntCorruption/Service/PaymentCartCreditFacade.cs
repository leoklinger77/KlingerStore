using KlingerStore.Payment.AntCorruption.Interfaces;
using KlingerStore.Payment.Domain.Class;
using KlingerStore.Payment.Domain.Interfaces;
using System.Threading.Tasks;

namespace KlingerStore.Payment.AntCorruption.Service
{
    public class PaymentCartCreditFacade : IPaymentCartCreditFacade
    {
        private readonly IPayPalGateway _payPalGateway;
        public readonly IConfigurationManager _configurationManager;

        public PaymentCartCreditFacade(IPayPalGateway payPalGateway, IConfigurationManager configurationManager)
        {
            _payPalGateway = payPalGateway;
            _configurationManager = configurationManager;
        }

        public async Task<Transaction> MakePayment(Order order, Domain.Class.Payment payment)
        {
            var apiKey = _configurationManager.GetValue("apiKey");
            var encriptionKey = _configurationManager.GetValue("encriptionKey");

            var serviceKey = _payPalGateway.GetPayPalServiceKey(apiKey, encriptionKey);
            var cardHashKey = _payPalGateway.GetCardHashKey(serviceKey, payment.NumberCart);

            var paymentResult = _payPalGateway.CommitTransaction(cardHashKey, order.OrderId.ToString(), payment.Value);

            var transaction = new Transaction
            {
                OrderId = order.OrderId,
                TotalValue = order.Value,
                PaymentId = payment.Id
            };

            if (paymentResult)
            {
                transaction.StatusTransaction = Domain.Class.Enum.StatusTransaction.Pago;
                return transaction;
            }
            transaction.StatusTransaction = Domain.Class.Enum.StatusTransaction.Recusado;
            return transaction;
        }
    }
}
