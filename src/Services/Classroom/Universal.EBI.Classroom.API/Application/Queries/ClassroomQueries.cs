using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universal.EBI.Classrooms.API.Application.Queries.Interfaces;
using Universal.EBI.Classrooms.API.Models;
using Universal.EBI.Classrooms.API.Models.Interfaces;

namespace Universal.EBI.Classrooms.API.Application.Queries
{
    public class ClassroomQueries : IClassroomQueries
    {
        
        private readonly IClassroomContext _context;

        public ClassroomQueries(IClassroomContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Classroom>> GetClassrooms(int pageSize, int pageIndex, string query = null)
        {
            
            query = string.IsNullOrEmpty(query) ? "" : query;
            var filter = new BsonDocument { { "Church", new BsonDocument { { "$regex", query }, { "$options", "i" } } } };

            var Classrooms = await _context.Classrooms.Find(filter).ToListAsync();

            var total = Classrooms.Count;
            var pageResult = new PagedResult<Classroom>
            {
                List = (IEnumerable<Classroom>)Classrooms,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };

            return pageResult;
        }       

        public async Task<Classroom> GetClassroomById(Guid id)
        {
            return await _context
                            .Classrooms
                            .Find(c => c.Id == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<Classroom> GetClassroomsByDateAndMeetingTime(DateTime date, string meetingTime)
        {
            return await _context
                            .Classrooms
                            .Find(c => c.CreatedDate == date && c.MeetingTime == meetingTime)
                            .FirstOrDefaultAsync();
        }
    }
       
}
