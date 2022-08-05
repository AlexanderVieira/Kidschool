using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Data;
using Universal.EBI.Core.Data.Interfaces;

namespace Universal.EBI.Childs.API.Models.Interfaces
{
    public interface IChildRepository : IRepository<Child>
    {
        Task<bool> CreateChild(Child child);
        Task<bool> UpdateChild(Child child);
        Task<bool> DeleteChild(Child child);
        Task<IDbContextTransaction> CriarTransacao();
        Task<IDbContextTransaction> CriarTransacao(IsolationLevel isolation);
        Task<ChildDbContext> GetContext();        
        //Task<Address> GetAddressById(Guid id);
        //Task CreateAddress(Address address);        

    }
}
