using EventStore.ClientAPI;
using KlingerStore.Core.Domain.Data.EventSource;
using KlingerStore.Core.Domain.Message;
using KlingerStore.EventSourc.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KlingerStore.EventSourc.Services
{
    public class EventSourceRepository : IEventSourceRepository
    {
        private readonly IEventStoreService _eventStore;

        public EventSourceRepository(IEventStoreService eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<IEnumerable<StoreEvent>> FindEvent(Guid aggregatedId)
        {
            var eventos = await _eventStore.GetConnection().ReadStreamEventsBackwardAsync(aggregatedId.ToString(), 0, 500, false);

            var listEventos = new List<StoreEvent>();

            foreach (var resolvedEvent in eventos.Events)
            {
                var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
                var jsonData = JsonConvert.DeserializeObject<Event>(dataEncoded);

                var evento = new StoreEvent(resolvedEvent.Event.EventId, resolvedEvent.Event.EventType, jsonData.Timestamp, dataEncoded);
                listEventos.Add(evento);
            }

            return listEventos;
        }

        public async Task SalveEvent<T>(T evento) where T : Event
        {
            try
            {
                await _eventStore.GetConnection().AppendToStreamAsync(
                evento.AggregateId.ToString(),
                ExpectedVersion.Any,
                FormatEvent(evento));
            }
            catch (Exception e)
            {
                
            }
            
        }

        private static IEnumerable<EventData> FormatEvent<T>(T evento) where T : Event
        {
            yield return new EventData(Guid.NewGuid(), evento.MessageType, true, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evento)), null);
        }
    }
}
