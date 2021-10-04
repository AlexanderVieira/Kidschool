using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universal.EBI.Educators.API.Models;

namespace Universal.EBI.Educators.API.Application.Queries.Interfaces
{
    public interface IEducatorQueries
    {
        Task<PagedResult<Educator>> GetEducators(int pageSize, int pageIndex, string query = null);
        Task<IEnumerable<Educator>> GetEducatorByName(string name);
        Task<Educator> GetEducatorByCpf(string cpf);
        Task<Educator> GetEducatorById(Guid id);
    }
}
