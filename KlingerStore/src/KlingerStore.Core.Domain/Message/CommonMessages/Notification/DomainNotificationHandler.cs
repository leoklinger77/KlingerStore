using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KlingerStore.Core.Domain.Message.CommonMessages.Notification
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private readonly List<DomainNotification> _notification;
        public DomainNotificationHandler()
        {
            _notification = new List<DomainNotification>();
        }
        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            _notification.Add(message);
            return Task.CompletedTask;
        }

        public virtual List<DomainNotification> FindNotification()
        {
            return _notification;
        }

        public virtual bool HasNotification()
        {
            return FindNotification().Any();
        }
        public void Dispose()
        {
            _notification.Clear();
        }
    }
}
