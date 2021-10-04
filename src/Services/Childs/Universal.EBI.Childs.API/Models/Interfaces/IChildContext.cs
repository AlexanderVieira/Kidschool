using MongoDB.Driver;
using Universal.EBI.Core.Data.Interfaces;

namespace Universal.EBI.Childs.API.Models.Interfaces
{
    public interface IChildContext : IUnitOfWork
    {        
        IMongoCollection<Child> Childs { get; }
    }
}
