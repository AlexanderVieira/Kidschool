using MongoDB.Driver;
using Universal.EBI.Core.Data.Interfaces;

namespace Universal.EBI.Responsible.API.Models.Interfaces
{
    public interface IResponsibleContext : IUnitOfWork
    {        
        IMongoCollection<Responsible> Responsibles { get; }
    }
}
