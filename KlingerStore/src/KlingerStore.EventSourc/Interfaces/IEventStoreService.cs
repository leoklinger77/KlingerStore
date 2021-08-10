using EventStore.ClientAPI;

namespace KlingerStore.EventSourc.Interfaces
{
    public interface IEventStoreService
    {
        IEventStoreConnection GetConnection();
    }
}
