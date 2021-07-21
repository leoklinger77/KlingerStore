using KlingerStore.Core.Domain.Message;
using KlingerStore.Core.Domain.Message.CommonMessages.Notification;
using System.Threading.Tasks;

namespace KlingerStore.Core.Domain.Communication.Mediatr
{
    public interface IMediatrHandler
    {
        Task PublishEvent<T>(T Tevent) where T : Event;
        Task<bool> SendCommand<T>(T command) where T : Command;
        Task PublishNotification<T>(T notification) where T : DomainNotification;
    }
}
