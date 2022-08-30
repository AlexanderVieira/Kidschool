using MongoDB.Driver;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;
using System.Threading.Tasks;
using System;

namespace Universal.EBI.Childs.API.Data.Repository
{
    public class SincDatabase : ISincDatabase
    {
        private readonly IChildContext _context;        

        public SincDatabase(IChildContext context)
        {
            _context = context;
        }

        public async Task<Child> CreateChild(Child child)
        {
            await _context.Children.InsertOneAsync(child);            
            return child;
        }

        public async Task<bool> UpdateChild(Child child)
        {
            var updateResult = await _context
                                        .Children
                                        .ReplaceOneAsync(filter: c => c.Id == child.Id, replacement: child);

            return updateResult.IsAcknowledged && updateResult.MatchedCount > 0;
        }

        public async Task<bool> DeleteChild(Guid id)
        {
            FilterDefinition<Child> filter = Builders<Child>.Filter.Eq(c => c.Id, id);
            DeleteResult deleteResult = await _context
                                                    .Children
                                                    .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public Task<IChildContext> GetContext()
        {
            return Task.FromResult(_context);
        }
    }
}
