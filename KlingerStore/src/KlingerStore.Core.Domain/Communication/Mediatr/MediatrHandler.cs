using KlingerStore.Core.Domain.Message;
using KlingerStore.Core.Domain.Message.CommonMessages.Notification;
using MediatR;
using System.Threading.Tasks;

namespace KlingerStore.Core.Domain.Communication.Mediatr
{
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediator _mediator;

        public MediatrHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T Tevent) where T : Event
        {
            await _mediator.Publish(Tevent);
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
