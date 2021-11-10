using System;
using System.Threading.Tasks;
using Universal.EBI.BFF.Report.API.Models;

namespace Universal.EBI.BFF.Report.API.Services.Interfaces
{
    public interface IChildService
    {
        Task<PagedResult<ChildDto>> GetChildren(int pageSize, int pageIndex, string query = null);
        Task<ChildDto> GetChildByCpf(string cpf);
        Task<ChildDto> GetChildById(Guid id);
    }
}
