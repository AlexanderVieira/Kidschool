using System;
using System.Threading.Tasks;

namespace Universal.EBI.Classrooms.API.Models.Interfaces
{
    public interface IClassroomRepository
    {
        Task CreateClassroom(Classroom Classroom);
        Task<bool> UpdateClassroom(Classroom Classroom);
        Task<bool> DeleteClassroom(Guid id);
        Task<IClassroomContext> GetContext();

    }
}
