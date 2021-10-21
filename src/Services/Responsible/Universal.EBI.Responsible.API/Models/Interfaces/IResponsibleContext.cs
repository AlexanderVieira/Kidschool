using MongoDB.Driver;
using Universal.EBI.Core.Data.Interfaces;
using Universal.EBI.Core.DomainObjects.Models;

namespace Universal.EBI.Responsibles.API.Models.Interfaces
{
    public interface IResponsibleContext : IUnitOfWork
    {        
        IMongoCollection<Responsible> Responsibles { get; }
    }
}
