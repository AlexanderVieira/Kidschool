using MongoDB.Driver;
using Universal.EBI.Responsibles.API.Models.Interfaces;
using System.Threading.Tasks;
using System;
using Universal.EBI.Core.Data.Interfaces;

namespace Universal.EBI.Responsibles.API.Data.Repository
{
    public class ResponsibleRepository : IResponsibleRepository
    {
        private readonly IResponsibleContext _context;        

        public IUnitOfWork UnitOfWork => _context;

        public ResponsibleRepository(IResponsibleContext context)
        {
            _context = context;
        }

        public async Task CreateResponsible(Models.Responsible responsible)
        {
            await _context.Responsibles.InsertOneAsync(responsible);
        }

        public async Task<bool> UpdateResponsible(Models.Responsible responsible)
        {
            var updateResult = await _context
                                        .Responsibles
                                        .ReplaceOneAsync(filter: r => r.Id == responsible.Id, replacement: responsible);

            return updateResult.IsAcknowledged && updateResult.MatchedCount > 0;
        }

        public async Task<bool> DeleteResponsible(Guid id)
        {
            FilterDefinition<Models.Responsible> filter = Builders<Models.Responsible>.Filter.Eq(r => r.Id, id);
            DeleteResult deleteResult = await _context
                                                    .Responsibles
                                                    .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public Task<IResponsibleContext> GetContext()
        {
            return Task.FromResult(_context);
        }

        public void Dispose()
        {
            //_context?.Commit().Dispose();
        }
    }
}
