using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Universal.EBI.Core.Data.Interfaces;

namespace Universal.EBI.Reports.API.Models.Interfaces
{
    public interface IResponsibleRepository : IRepository<Responsible>
    {        
        Task<IEnumerable<Responsible>> GetResponsibles();
        Task<IEnumerable<Responsible>> GetResponsiblesByName(string name);
        Task<Responsible> GetResponsibleByCpf(string cpf);        
        Task<Responsible> GetResponsibleById(Guid id);
        Task CreateResponsible(Responsible Responsible);
        Task UpdateResponsible(Responsible Responsible);
        Task DeleteResponsible(Guid id);
        Task<Address> GetAddressById(Guid id);
        Task CreateAddress(Address address);
        DbConnection GetConnection();

    }
}
