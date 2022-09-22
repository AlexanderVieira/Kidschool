using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Universal.EBI.Core.Comunication;
using Universal.EBI.MVC.Models;

namespace Universal.EBI.MVC.Services.Interfaces
{
    public interface IReportBffService
    {
        Task<PagedResult<EducatorClassroomViewModel>> GetEducators(int pageSize, int pageIndex, string query = null);       
        Task<EducatorViewModel> GetEducatorByCpf(string cpf);
        Task<EducatorViewModel> GetEducatorById(Guid id);
        Task<ObjectResult> GetChildren(int pageSize, int pageIndex, string query = null);
        Task<ObjectResult> GetChildrenInactives(int pageSize, int pageIndex, string query = null);
        Task<ChildResponseViewModel> GetChildByCpf(string cpf);
        Task<ChildResponseViewModel> GetChildById(Guid id);
        Task<ResponseResult> CreateChild(ChildRequestViewModel request);
        Task<ResponseResult> UpdateChild(ChildRequestViewModel request);
        Task<ResponseResult> DeleteChild(Guid id);
        Task<ResponseResult> ActivateChild(ChildRequestViewModel request);
        Task<ResponseResult> InactivateChild(ChildRequestViewModel request);
        Task<ResponseResult> AddResponsible(AddResponsibleRequestViewModel request);
        Task<ResponseResult> DeleteResponsible(DeleteResponsibleRequestViewModel request);
        Task<ResponseResult> CreateClassroom(ClassroomViewModel classroom);
        Task<ResponseResult> UpdateClassroom(ClassroomViewModel classroom);
        Task<ClassroomViewModel> GetClassroomById(Guid id);
        Task<ObjectResult> GetClassrooms(int pageSize, int pageIndex, string query = null);
    }
}
