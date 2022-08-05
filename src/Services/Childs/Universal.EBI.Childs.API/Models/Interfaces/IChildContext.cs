using MongoDB.Driver;

namespace Universal.EBI.Childs.API.Models.Interfaces
{
    public interface IChildContext
    {        
        IMongoCollection<Child> Children { get; }
    }
}
