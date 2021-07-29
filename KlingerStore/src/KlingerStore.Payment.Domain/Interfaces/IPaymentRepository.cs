using KlingerStore.Core.Domain.Data.Interfaces;
using KlingerStore.Payment.Domain.Class;
using System.Threading.Tasks;

namespace KlingerStore.Payment.Domain.Interfaces
{
    public interface IPaymentRepository : IRepository<Class.Payment>
    {
        Task Insert(Class.Payment payment);
        Task InsertTransaction(Transaction transaction);
    }
}
