using System;

namespace KlingerStore.Core.Domain.Data.EventSource
{
    public class StoreEvent
    {
        public Guid Id { get; private set; }
        public string Type { get; private set; }
        public DateTime DataOcurrent { get; set; }
        public string Data { get; private set; }

        public StoreEvent(Guid id, string type, DateTime dataOcurrent, string data)
        {
            Id = id;
            Type = type;
            DataOcurrent = dataOcurrent;
            Data = data;
        }
    }
}
