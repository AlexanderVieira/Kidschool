using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universal.EBI.Responsible.API.Models;

namespace Universal.EBI.Responsible.API.Application.Queries.Interfaces
{
    public interface IResponsibleQueries
    {
        Task<PagedResult<Models.Responsible>> GetResponsibles(int pageSize, int pageIndex, string query = null);
        Task<IEnumerable<Models.Responsible>> GetResponsiblesByName(string name);
        Task<Models.Responsible> GetResponsibleByCpf(string cpf);
        Task<Models.Responsible> GetResponsibleById(Guid id);
    }
}
