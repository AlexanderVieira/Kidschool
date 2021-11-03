using System;
using System.Threading.Tasks;
using Universal.EBI.BFF.Report.API.Models;

namespace Universal.EBI.BFF.Report.API.Services.Interfaces
{
    public interface IEducatorService
    {
        Task<PagedResult<EducatorClassroomDto>> GetEducators(int pageSize, int pageIndex, string query = null);
        Task<EducatorDto> GetEducatorByCpf(string cpf);
        Task<EducatorDto> GetEducatorById(Guid id);
    }
}
