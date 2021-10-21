using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Universal.EBI.Core.Data.Interfaces;
using Universal.EBI.Core.DomainObjects.Models;

namespace Universal.EBI.Reports.API.Models.Interfaces
{
    public interface IEducatorRepository : IRepository<Educator>
    {        
        Task<IEnumerable<Educator>> GetEducators();
        Task<IEnumerable<Educator>> GetEducatorByName(string name);
        Task<Educator> GetEducatorByCpf(string cpf);        
        Task<Educator> GetEducatorById(Guid id);
        Task CreateEducator(Educator educator);
        Task UpdateEducator(Educator educator);
        Task DeleteEducator(Guid id);
        Task<Address> GetAddressById(Guid id);
        Task CreateAddress(Address address);
        DbConnection GetConnection();

    }
}
