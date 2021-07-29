using KlingerStore.Core.Domain.Data.Interfaces;
using KlingerStore.Payment.Data.Context;
using KlingerStore.Payment.Domain.Class;
using KlingerStore.Payment.Domain.Interfaces;
using System.Threading.Tasks;

namespace KlingerStore.Payment.Data.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentContext _context;

        public PaymentRepository(PaymentContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;        

        public async Task Insert(Domain.Class.Payment payment)
        {
            await _context.Payment.AddAsync(payment);
        }

        public async Task InsertTransaction(Transaction transaction)
        {
            await _context.Transaction.AddAsync(transaction);
        }
        public void Dispose()
        {
            _context?.DisposeAsync();
        }
    }
}
