using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Models;

namespace Universal.EBI.Childs.API.Application.Queries.Interfaces
{
    public interface IChildQueries
    {
        Task<PagedResult<Child>> GetChilds(int pageSize, int pageIndex, string query = null);
        Task<IEnumerable<Child>> GetChildsByName(string name);
        Task<Child> GetChildByCpf(string cpf);
        Task<Child> GetChildById(Guid id);
    }
}
