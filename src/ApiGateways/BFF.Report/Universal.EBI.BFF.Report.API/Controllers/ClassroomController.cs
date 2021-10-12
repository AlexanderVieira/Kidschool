using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Universal.EBI.BFF.Report.API.Models;
using Universal.EBI.BFF.Report.API.Services.Interfaces;
using Universal.EBI.WebAPI.Core.Controllers;

namespace Universal.EBI.BFF.Report.API.Controllers
{
    public class ClassroomController : BaseController
    {
        private readonly IClassroomService _classroomService;

        public ClassroomController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        //private readonly IResponsibleService _responsibleService;
        //private readonly IChildService _childService;
        //private readonly IEducatorService _educatorService;

        //public ClassroomController(IClassroomService classroomService, 
        //                           IResponsibleService responsibleService, 
        //                           IChildService childService, 
        //                           IEducatorService educatorService)
        //{
        //    _classroomService = classroomService;
        //    _responsibleService = responsibleService;
        //    _childService = childService;
        //    _educatorService = educatorService;
        //}

        [HttpGet]
        [Route("reports/classrooms")]
        public async Task<PagedResultDto<ClassroomDto>> GetClassrooms([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            return await _classroomService.GetClassrooms(ps, page, q);
        }

        [HttpPost]
        [Route("reports/classroom/create")]
        public async Task<IActionResult> CreateClassroom(ClassroomDto classroomDto)
        {
            var response = await _classroomService.CreateClassroom(classroomDto);
            return CustomResponse(response);
        }
    }
}
