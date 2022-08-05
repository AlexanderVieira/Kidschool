using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Data;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.Mediator.Interfaces;

namespace Universal.EBI.Childs.API.Extensions
{
    public static class MediatorExtension
    {
        public static async Task PublishEvents<T>(this IMediatorHandler mediator, T context) where T : ChildContext
        {
            //FilterDefinition<Child> filter = Builders<Child>.Filter.ElemMatch(x => x.Notifications, y => y.AggregateId == System.Guid.Empty);
            FilterDefinition<Child> filter = Builders<Child>.Filter.Where(x => x.Notifications != null && x.Notifications.Any());
            var domainEntities = await context
                                        .Children
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

        public static async Task PublishEvents_v2<T>(this IMediatorHandler mediator, T context) where T : DbContext
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            domainEntities.ToList()
                .ForEach(e => e.Entity.ClearEvent());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
