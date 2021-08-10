using EventStore.ClientAPI;
using KlingerStore.EventSourc.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KlingerStore.EventSourc.Services
{
    public class EventStoreService : IEventStoreService
    {        
        private readonly IEventStoreConnection _Connecton;        

        public EventStoreService(IConfiguration configuration)
        {
            _Connecton = EventStoreConnection.Create(
                configuration.GetConnectionString("EventStoreConnection"));
            _Connecton.ConnectAsync();
        }

        public IEventStoreConnection GetConnection() => _Connecton;        
    }
}
