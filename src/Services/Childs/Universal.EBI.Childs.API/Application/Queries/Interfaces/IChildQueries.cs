using System;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Models;

namespace Universal.EBI.Childs.API.Application.Queries.Interfaces
{
    public interface IChildQueries
    {
        Task<PagedResult<ChildDesignedQuery>> GetChildren(int pageSize, int pageIndex, string query = null); 
        Task<PagedResult<ChildDesignedQuery>> GetChildrenInactives(int pageSize, int pageIndex, string query = null);
        Task<Child> GetChildByCpf(string cpf);
        Task<Child> GetChildById(Guid id);
    }
}
