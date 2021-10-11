using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universal.EBI.Responsibles.API.Models;

namespace Universal.EBI.Responsibles.API.Application.Queries.Interfaces
{
    public interface IResponsibleQueries
    {
        Task<PagedResult<Models.Responsible>> GetResponsibles(int pageSize, int pageIndex, string query = null);
        Task<IEnumerable<Models.Responsible>> GetResponsiblesByName(string name);
        Task<Models.Responsible> GetResponsibleByCpf(string cpf);
        Task<Models.Responsible> GetResponsibleById(Guid id);
    }
}
