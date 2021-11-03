using System;
using System.Threading.Tasks;
using Universal.EBI.MVC.Models;

namespace Universal.EBI.MVC.Services.Interfaces
{
    public interface IReportBffService
    {
        Task<PagedResult<EducatorClassroomViewModel>> GetEducators(int pageSize, int pageIndex, string query = null);       
        Task<EducatorViewModel> GetEducatorByCpf(string cpf);
        Task<EducatorViewModel> GetEducatorById(Guid id);
    }
}
