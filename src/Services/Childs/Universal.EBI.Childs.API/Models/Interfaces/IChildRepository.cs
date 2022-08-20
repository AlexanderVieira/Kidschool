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
        Task<Child> GetChildById(Guid id);
        Task<bool> CreateChild(Child child);
        Task<bool> UpdateChild(Child child);
        Task<bool> DeleteChild(Child child);
        Task<bool> ActivateChild(Child child);
        Task<bool> InactivateChild(Child child);
        Task<bool> AddResponsible(Child child);
        Task<IDbContextTransaction> CriarTransacao();
        Task<IDbContextTransaction> CriarTransacao(IsolationLevel isolation);
        Task<ChildDbContext> GetContext();
    }
}
