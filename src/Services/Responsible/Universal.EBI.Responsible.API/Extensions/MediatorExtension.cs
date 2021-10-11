using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using Universal.EBI.Responsibles.API.Data;
using Universal.EBI.Core.Mediator.Interfaces;

namespace Universal.EBI.Responsibles.API.Extensions
{
    public static class MediatorExtension
    {
        public static async Task PublishEvents<T>(this IMediatorHandler mediator, T context) where T : ResponsibleContext
        {
            //FilterDefinition<Child> filter = Builders<Child>.Filter.ElemMatch(x => x.Notifications, y => y.AggregateId == System.Guid.Empty);
            FilterDefinition<Models.Responsible> filter = Builders<Models.Responsible>.Filter.Where(x => x.Notifications != null && x.Notifications.Any());
            var domainEntities = await context
                                        .Responsibles
                                        .Find(filter)
                                        .ToListAsync();

            var domainEvents = domainEntities
                .SelectMany(x => x.Notifications)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.ClearEvent());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
