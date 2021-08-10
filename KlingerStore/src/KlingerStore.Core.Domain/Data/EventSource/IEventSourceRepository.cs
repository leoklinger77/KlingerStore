using KlingerStore.Core.Domain.Message;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KlingerStore.Core.Domain.Data.EventSource
{
    public interface IEventSourceRepository
    {
        Task SalveEvent<T>(T evento) where T : Event;
        Task<IEnumerable<StoreEvent>> FindEvent(Guid aggregatedId);
    }
}
