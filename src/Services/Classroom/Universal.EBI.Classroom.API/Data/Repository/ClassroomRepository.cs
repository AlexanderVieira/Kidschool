using MongoDB.Driver;
using Universal.EBI.Classrooms.API.Models.Interfaces;
using System.Threading.Tasks;
using System;
using Universal.EBI.Core.Data.Interfaces;
using Universal.EBI.Classrooms.API.Models;

namespace Universal.EBI.Classrooms.API.Data.Repository
{
    public class ClassroomRepository : IClassroomRepository
    {
        private readonly IClassroomContext _context;

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public ClassroomRepository(IClassroomContext context)
        {
            _context = context;
        }

        public async Task CreateClassroom(Classroom classroom)
        {
            await _context.Classrooms.InsertOneAsync(classroom);
        }

        public async Task<bool> UpdateClassroom(Classroom classroom)
        {
            var updateResult = await _context
                                        .Classrooms
                                        .ReplaceOneAsync(filter: r => r.Id == classroom.Id, replacement: classroom);

            return updateResult.IsAcknowledged && updateResult.MatchedCount > 0;
        }

        public async Task<bool> DeleteClassroom(Guid id)
        {
            FilterDefinition<Classroom> filter = Builders<Classroom>.Filter.Eq(r => r.Id, id);
            DeleteResult deleteResult = await _context
                                                    .Classrooms
                                                    .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public Task<IClassroomContext> GetContext()
        {
            return Task.FromResult(_context);
        }

        public void Dispose()
        {
            //_context?.Commit().Dispose();
        }
    }
}
