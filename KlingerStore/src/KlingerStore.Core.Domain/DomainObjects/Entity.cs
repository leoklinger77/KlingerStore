using KlingerStore.Core.Domain.Message;
using System;
using System.Collections.Generic;

namespace KlingerStore.Core.Domain.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        private List<Event> _notifier;
        public IReadOnlyCollection<Event> Notifier => _notifier?.AsReadOnly();

        protected Entity()
        {
            Id = Guid.NewGuid();
            _notifier = new List<Event>();
        }
        public IReadOnlyCollection<Event> GetEvents() => _notifier;
        public void AddEvent(Event _event)
        {
            _notifier.Add(_event);
        }
        public void RemoveEvent(Event _event)
        {
            _notifier.Remove(_event);
        }
        public void DisposeEvent()
        {
            _notifier?.Clear();
        }
        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }
        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 956) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
