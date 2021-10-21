using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Responsibles.API.Models;

namespace Universal.EBI.Responsibles.API.Application.Queries.Interfaces
{
    public interface IResponsibleQueries
    {
        Task<PagedResult<Responsible>> GetResponsibles(int pageSize, int pageIndex, string query = null);
        Task<IEnumerable<Responsible>> GetResponsiblesByName(string name);
        Task<Responsible> GetResponsibleByCpf(string cpf);
        Task<Responsible> GetResponsibleById(Guid id);
    }
}
