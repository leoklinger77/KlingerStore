using KlingerStore.Core.Domain.Communication.Mediatr;
using KlingerStore.Core.Domain.DomainObjects;
using System.Linq;
using System.Threading.Tasks;

namespace KlingerStore.Sales.Data.Context
{
    public static class MediatorExtension
    {
        public static async Task SendEvent(this IMediatrHandler mediatr, SalesContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notifier != null && x.Entity.Notifier.Any());

            var domainEvents = domainEntities.SelectMany(x => x.Entity.Notifier).ToList();

            domainEntities.ToList().ForEach(x => x.Entity.DisposeEvent());

            var tasks = domainEvents.Select(async (domainEvents) =>
            {
                await mediatr.PublishEvent(domainEvents);
            });

            await Task.WhenAll(tasks);
        }
    }
}
