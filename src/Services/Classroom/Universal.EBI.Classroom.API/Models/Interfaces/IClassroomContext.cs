using MongoDB.Driver;

namespace Universal.EBI.Classrooms.API.Models.Interfaces
{
    public interface IClassroomContext
    {        
        IMongoCollection<Classroom> Classrooms { get; }
    }
}
