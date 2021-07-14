using System.Threading.Tasks;

namespace KlingerStore.Core.Domain.Data.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
