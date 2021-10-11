using System;
using System.Threading.Tasks;
using Universal.EBI.Core.Data.Interfaces;

namespace Universal.EBI.Classrooms.API.Models.Interfaces
{
    public interface IClassroomRepository : IRepository<Classroom>
    {
        Task CreateClassroom(Classroom Classroom);
        Task<bool> UpdateClassroom(Classroom Classroom);
        Task<bool> DeleteClassroom(Guid id);
        Task<IClassroomContext> GetContext();
        
        //Task<Address> GetAddressById(Guid id);
        //Task CreateAddress(Address address);        

    }
}
