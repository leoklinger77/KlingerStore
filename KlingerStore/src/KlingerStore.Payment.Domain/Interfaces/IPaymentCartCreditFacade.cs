using KlingerStore.Payment.Domain.Class;
using System.Threading.Tasks;

namespace KlingerStore.Payment.Domain.Interfaces
{
    public interface IPaymentCartCreditFacade
    {
        Task<Class.Transaction> MakePayment(Order order, Class.Payment payment);
    }
}
