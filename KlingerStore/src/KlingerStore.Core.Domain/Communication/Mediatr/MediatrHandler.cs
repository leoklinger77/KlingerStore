using KlingerStore.Core.Domain.Data.EventSource;
using KlingerStore.Core.Domain.Message;
using KlingerStore.Core.Domain.Message.CommonMessages.Notification;
using MediatR;
using System.Threading.Tasks;

namespace KlingerStore.Core.Domain.Communication.Mediatr
{
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventSourceRepository _eventSource;
        public MediatrHandler(IMediator mediator, IEventSourceRepository eventSource)
        {
            _mediator = mediator;
            _eventSource = eventSource;
        }

        public async Task PublishEvent<T>(T Tevent) where T : Event
        {
            await _mediator.Publish(Tevent);

            if (!Tevent.GetType().BaseType.Name.Equals("DomainEvent")) await _eventSource.SalveEvent(Tevent);
        }

        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }

        public async Task<bool> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}
