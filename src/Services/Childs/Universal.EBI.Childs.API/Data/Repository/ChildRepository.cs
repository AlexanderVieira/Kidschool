using MongoDB.Driver;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Universal.EBI.Core.Data.Interfaces;

namespace Universal.EBI.Childs.API.Data.Repository
{
    public class ChildRepository : IChildRepository
    {
        private readonly IChildContext _context;        

        public IUnitOfWork UnitOfWork => _context;

        public ChildRepository(IChildContext context)
        {
            _context = context;
        }

        public async Task CreateChild(Child child)
        {
            await _context.Childs.InsertOneAsync(child);
        }

        public async Task<bool> UpdateChild(Child child)
        {
            var updateResult = await _context
                                        .Childs
                                        .ReplaceOneAsync(filter: c => c.Id == child.Id, replacement: child);

            return updateResult.IsAcknowledged && updateResult.MatchedCount > 0;
        }

        public async Task<bool> DeleteChild(Guid id)
        {
            FilterDefinition<Child> filter = Builders<Child>.Filter.Eq(c => c.Id, id);
            DeleteResult deleteResult = await _context
                                                    .Childs
                                                    .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public Task<IChildContext> GetContext()
        {
            return Task.FromResult(_context);
        }

        public void Dispose()
        {
            //_context?.Commit().Dispose();
        }
    }
}
