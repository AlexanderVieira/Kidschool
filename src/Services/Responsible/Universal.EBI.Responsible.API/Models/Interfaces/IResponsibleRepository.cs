using System;
using System.Threading.Tasks;
using Universal.EBI.Core.Data.Interfaces;
using Universal.EBI.Core.DomainObjects.Models;

namespace Universal.EBI.Responsibles.API.Models.Interfaces
{
    public interface IResponsibleRepository : IRepository<Responsible>
    {
        Task CreateResponsible(Responsible responsible);
        Task<bool> UpdateResponsible(Responsible responsible);
        Task<bool> DeleteResponsible(Guid id);
        Task<IResponsibleContext> GetContext();

    }
}
