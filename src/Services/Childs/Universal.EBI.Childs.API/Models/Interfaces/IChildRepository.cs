using System;
using System.Threading.Tasks;
using Universal.EBI.Core.Data.Interfaces;

namespace Universal.EBI.Childs.API.Models.Interfaces
{
    public interface IChildRepository : IRepository<Child>
    {
        Task CreateChild(Child child);
        Task<bool> UpdateChild(Child child);
        Task<bool> DeleteChild(Guid id);
        Task<IChildContext> GetContext();
        
        //Task<Address> GetAddressById(Guid id);
        //Task CreateAddress(Address address);        

    }
}
