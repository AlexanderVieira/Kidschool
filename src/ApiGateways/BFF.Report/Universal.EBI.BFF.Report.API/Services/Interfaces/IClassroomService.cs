using System;
using System.Threading.Tasks;
using Universal.EBI.BFF.Report.API.Models;
using Universal.EBI.Core.Comunication;

namespace Universal.EBI.BFF.Report.API.Services.Interfaces
{
    public interface IClassroomService
    {
        Task<PagedResult<ClassroomDto>> GetClassrooms(int pageSize, int pageIndex, string query = null);
        Task<ClassroomDto> GetClassroomById(Guid id);
        Task<ResponseResult> CreateClassroom(ClassroomDto classroom);
        Task<ResponseResult> UpdateClassroom(ClassroomDto classroom);
        Task<ResponseResult> DeleteClassroom(Guid id);
        Task<ResponseResult> AddChildsClassroom(ClassroomDto classroom);
        Task<ResponseResult> DeleteChildsClassroom(DeleteChildClassroomDto deleteChildClassroomDto);
    }
}
