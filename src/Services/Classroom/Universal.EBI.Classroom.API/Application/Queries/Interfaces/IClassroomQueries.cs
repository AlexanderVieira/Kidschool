using System;
using System.Threading.Tasks;
using Universal.EBI.Classrooms.API.Application.DTOs;
using Universal.EBI.Classrooms.API.Models;

namespace Universal.EBI.Classrooms.API.Application.Queries.Interfaces
{
    public interface IClassroomQueries
    {
        Task<PagedResult<ClassroomResponseDto>> GetClassrooms(int pageSize, int pageIndex, string query = null);
        Task<Classroom> GetClassroomsByDateAndMeetingTime(DateTime date, string meetingTime);        
        Task<Classroom> GetClassroomById(Guid id);
    }
}
