using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Universal.EBI.Classrooms.API.Application.DTOs;
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

        public async Task<PagedResult<ClassroomResponseDto>> GetClassrooms(int pageSize, int pageIndex, string query = null)
        {
            
            query = string.IsNullOrEmpty(query) ? "" : query;
            var filter = new BsonDocument { { "Church", new BsonDocument { { "$regex", query }, { "$options", "i" } } } };

            var collection = await _context.Classrooms.Find(filter).ToListAsync();

            //var total = Classrooms.Count;
            //var pageResult = new PagedResult<Classroom>
            //{
            //    List = Classrooms,
            //    TotalResults = total,
            //    PageIndex = pageIndex,
            //    PageSize = pageSize,
            //    Query = query
            //};
                        
            var classrooms = collection.Where(x => x.Actived == true).Select(x =>
               new ClassroomResponseDto
               {
                   Id = x.Id,
                   Church = x.Church,
                   Region = x.Region,
                   Lunch = x.Lunch,
                   MeetingTime = x.MeetingTime,
                   ClassroomType = x.ClassroomType.ToString(),
                   Educator = new EducatorDto 
                   { 
                       Id = x.Educator.Id,
                       FirstName = x.Educator.FirstName,
                       LastName = x.Educator.LastName,
                       FunctionType = x.Educator.FunctionType.ToString(),
                       PhotoUrl = x.Educator.PhotoUrl
                   }

               }).ToList();
            var total = classrooms.Count;
            var pageResult = new PagedResult<ClassroomResponseDto>
            {
                List = classrooms,
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
