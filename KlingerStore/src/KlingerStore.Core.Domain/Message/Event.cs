using MediatR;
using System;

namespace KlingerStore.Core.Domain.Message
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; set; }
        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
