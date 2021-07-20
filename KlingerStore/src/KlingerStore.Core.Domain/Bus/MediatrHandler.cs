using KlingerStore.Core.Domain.Message;
using MediatR;
using System.Threading.Tasks;

namespace KlingerStore.Core.Domain.Bus
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

        public async Task<bool> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}
