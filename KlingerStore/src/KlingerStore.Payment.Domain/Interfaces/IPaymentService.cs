using KlingerStore.Core.Domain.DomainObjects.DTOs;
using System.Threading.Tasks;

namespace KlingerStore.Payment.Domain.Interfaces
{
    public interface IPaymentService
    {
        Task<Class.Transaction> MakePaymentOnRequest(PaymentOrder paymententOrder);
    }
}
