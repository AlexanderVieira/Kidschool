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
        public async Task<PagedResult<ChildDto>> GetChildren([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var pagedResultchild = await _childService.GetChildren(ps, page, q);            
            return pagedResultchild;
        }

        [HttpGet]
        [Route("reports/child/{id}")]
        public async Task<ActionResult<ChildDto>> GetchildById(Guid id)
        {
            var child = await _childService.GetChildById(id);
            return child == null ? NotFound() : CustomResponse(child);
        }

        [HttpGet("reports/child/{cpf:length(11)}")]
        public async Task<ActionResult<ChildDto>> GetchildByCpf(string cpf)
        {
            var child = await _childService.GetChildByCpf(cpf);
            return child == null ? NotFound() : CustomResponse(child);
        }
        
    }
}
