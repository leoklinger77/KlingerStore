using KlingerStore.Core.Domain.Message;
using System.Threading.Tasks;

namespace KlingerStore.Core.Domain.Bus
{
    public interface IMediatrHandler
    {
        Task PublishEvent<T>(T Tevent) where T : Event;
    }
}
