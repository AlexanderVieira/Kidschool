using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Universal.EBI.BFF.Report.API.Models;
using Universal.EBI.BFF.Report.API.Services.Interfaces;
using Universal.EBI.WebAPI.Core.Controllers;

namespace Universal.EBI.BFF.Report.API.Controllers
{
    public class EducatorController : BaseController
    {
        private readonly IEducatorService _educatorService;

        public EducatorController(IEducatorService educatorService)
        {
            _educatorService = educatorService;
        }

        [HttpGet]
        [Route("reports/educators")]
        public async Task<PagedResult<EducatorClassroomDto>> GetEducators([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var pagedResultClassroomDto = await _educatorService.GetEducators(ps, page, q);            
            return pagedResultClassroomDto;
        }

        [HttpGet]
        [Route("reports/educator/{id}")]
        public async Task<IActionResult> GetEducatorById(Guid id)
        {
            var educator = await _educatorService.GetEducatorById(id);
            return educator == null ? NotFound() : CustomResponse(educator);
        }

        [HttpGet("reports/educator/{cpf:length(11)}", Name = "GetEducatorByCpf")]
        public async Task<ActionResult<EducatorDto>> GetEducatorByCpf(string cpf)
        {
            var educator = await _educatorService.GetEducatorByCpf(cpf);
            return educator == null ? NotFound() : CustomResponse(educator);
        }
        
    }
}
