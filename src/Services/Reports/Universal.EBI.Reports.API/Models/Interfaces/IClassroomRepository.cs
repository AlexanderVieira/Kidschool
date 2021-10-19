using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universal.EBI.Core.Data.Interfaces;

namespace Universal.EBI.Reports.API.Models.Interfaces
{
    public interface IClassroomRepository : IRepository<Classroom>
    {
        Task CreateClassroom(Classroom Classroom);
        Task UpdateClassroom(Classroom Classroom);
        Task DeleteClassroom(Guid id);
        Task<Classroom> GetClassroomById(Guid id);
        Task<IEnumerable<Classroom>> GetClassroomByDate(DateTime inicialDate, DateTime finalDate);

    }
}
