using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Universal.EBI.Core.Data.Interfaces;

namespace Universal.EBI.Childs.API.Models.Interfaces
{
    public interface IChildRepository : IRepository<Child>
    {        
        Task<IEnumerable<Child>> GetChilds(int pageSize, int pageIndex, string query = null);        
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
