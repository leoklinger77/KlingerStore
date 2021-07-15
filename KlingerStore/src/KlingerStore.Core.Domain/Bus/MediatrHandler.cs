using KlingerStore.Core.Domain.Interfaces;
using KlingerStore.Core.Domain.Message;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
