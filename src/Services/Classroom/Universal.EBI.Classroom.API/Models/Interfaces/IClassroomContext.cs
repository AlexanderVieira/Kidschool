using MongoDB.Driver;
using Universal.EBI.Core.Data.Interfaces;

namespace Universal.EBI.Classrooms.API.Models.Interfaces
{
    public interface IClassroomContext : IUnitOfWork
    {        
        IMongoCollection<Classroom> Classrooms { get; }
    }
}
