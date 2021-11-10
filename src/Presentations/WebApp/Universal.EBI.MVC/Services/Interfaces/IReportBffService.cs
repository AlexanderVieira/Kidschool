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
        Task<PagedResult<ChildViewModel>> GetChildren(int pageSize, int pageIndex, string query = null);
        Task<ChildViewModel> GetChildByCpf(string cpf);
        Task<ChildViewModel> GetChildById(Guid id);
    }
}
