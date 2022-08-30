using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Universal.EBI.BFF.Report.API.Models;
using Universal.EBI.BFF.Report.API.Services.Interfaces;
using Universal.EBI.WebAPI.Core.Controllers;

namespace Universal.EBI.BFF.Report.API.Controllers
{
    public class ChildController : BaseController
    {
        private readonly IChildService _childService;

        public ChildController(IChildService childService)
        {
            _childService = childService;
        }

        [HttpGet]
        [Route("reports/children")]
        public async Task<ActionResult> GetChildren([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            try
            {
                var response = await _childService.GetChildren(ps, page, q);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        [HttpGet]
        [Route("reports/child/{id}")]
        public async Task<ActionResult<ChildResponseDto>> GetchildById(Guid id)
        {
            var child = await _childService.GetChildById(id);
            return child == null ? NotFound() : CustomResponse(child);
        }

        [HttpGet("reports/child/{cpf:length(11)}")]
        public async Task<ActionResult<ChildResponseDto>> GetchildByCpf(string cpf)
        {
            var child = await _childService.GetChildByCpf(cpf);
            return child == null ? NotFound() : CustomResponse(child);
        }
        
    }
}
