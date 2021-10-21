using MongoDB.Driver;
using Universal.EBI.Core.Data.Interfaces;
using Universal.EBI.Core.DomainObjects.Models;

namespace Universal.EBI.Childs.API.Models.Interfaces
{
    public interface IChildContext : IUnitOfWork
    {        
        IMongoCollection<Child> Childs { get; }
    }
}
