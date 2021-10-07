using System;
using System.Threading.Tasks;
using Universal.EBI.Core.Data.Interfaces;

namespace Universal.EBI.Responsible.API.Models.Interfaces
{
    public interface IResponsibleRepository : IRepository<Responsible>
    {
        Task CreateResponsible(Responsible responsible);
        Task<bool> UpdateResponsible(Responsible responsible);
        Task<bool> DeleteResponsible(Guid id);
        Task<IResponsibleContext> GetContext();
        
        //Task<Address> GetAddressById(Guid id);
        //Task CreateAddress(Address address);        

    }
}
