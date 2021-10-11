using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universal.EBI.Classrooms.API.Models;

namespace Universal.EBI.Classrooms.API.Application.Queries.Interfaces
{
    public interface IClassroomQueries
    {
        Task<PagedResult<Classroom>> GetClassrooms(int pageSize, int pageIndex, string query = null);
        Task<IEnumerable<Classroom>> GetClassroomsByName(string name);        
        Task<Classroom> GetClassroomById(Guid id);
    }
}
