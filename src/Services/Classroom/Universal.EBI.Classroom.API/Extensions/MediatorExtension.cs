using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using Universal.EBI.Classrooms.API.Data;
using Universal.EBI.Classrooms.API.Models;
using Universal.EBI.Core.Mediator.Interfaces;

namespace Universal.EBI.Classrooms.API.Extensions
{
    public static class MediatorExtension
    {
        public static async Task PublishEvents<T>(this IMediatorHandler mediator, T context) where T : ClassroomContext
        {            
            FilterDefinition<Classroom> filter = Builders<Classroom>.Filter.Where(x => x.Notifications != null && x.Notifications.Any());
            var domainEntities = await context
                                        .Classrooms
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
