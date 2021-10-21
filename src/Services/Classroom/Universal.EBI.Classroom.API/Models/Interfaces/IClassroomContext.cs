using MongoDB.Driver;
using Universal.EBI.Core.Data.Interfaces;
using Universal.EBI.Core.DomainObjects.Models;

namespace Universal.EBI.Classrooms.API.Models.Interfaces
{
    public interface IClassroomContext : IUnitOfWork
    {        
        IMongoCollection<Classroom> Classrooms { get; }
    }
}
