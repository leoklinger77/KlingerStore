using KlingerStore.Core.Domain.DomainObjects;
using System;

namespace KlingerStore.Core.Domain.Data.Interfaces
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
