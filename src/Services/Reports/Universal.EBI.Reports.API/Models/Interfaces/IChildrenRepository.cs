using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Universal.EBI.Core.Data.Interfaces;
using Universal.EBI.Core.DomainObjects.Models;

namespace Universal.EBI.Reports.API.Models.Interfaces
{
    public interface IChildrenRepository : IRepository<Child>
    {        
        Task<IEnumerable<Child>> GetChildren();
        Task<IEnumerable<Child>> GetChildrenByName(string name);
        Task<Child> GetChildByCpf(string cpf);        
        Task<Child> GetChildById(Guid id);
        Task CreateChild(Child Child);
        Task UpdateChild(Child Child);
        Task DeleteChild(Guid id);
        Task<Address> GetAddressById(Guid id);
        Task CreateAddress(Address address);
        DbConnection GetConnection();

    }
}
