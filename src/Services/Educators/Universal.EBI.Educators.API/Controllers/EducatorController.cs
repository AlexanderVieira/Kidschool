using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Educators.API.Application.Commands;
using Universal.EBI.Educators.API.Application.Queries.Interfaces;
using Universal.EBI.Educators.API.Models;
using Universal.EBI.WebAPI.Core.AspNetUser.Interfaces;
using Universal.EBI.WebAPI.Core.Controllers;

namespace Universal.EBI.Educators.API.Controllers
{
    public class EducatorController : BaseController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IAspNetUser _user;
        private readonly IEducatorQueries _educatorQueries;

        public EducatorController(IMediatorHandler mediator, IAspNetUser user, IEducatorQueries educatorQueries)
        {
            _mediator = mediator;
            _user = user;
            _educatorQueries = educatorQueries;
        }

        [HttpGet("api/educators")]
        public async Task<IActionResult> GetEducators([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var educator = await _educatorQueries.GetEducators(ps, page, q);
            return educator == null ? NotFound() : CustomResponse(educator);
        }
        
        [HttpGet("api/educator/{id}")]
        public async Task<IActionResult> GetEducatorById(Guid id)
        {
            var educator = await _educatorQueries.GetEducatorById(id);
            return educator == null ? NotFound() : CustomResponse(educator);
        }

        [HttpGet("api/educator/{cpf:length(11)}", Name = "GetEducatorByCpf")]
        public async Task<ActionResult<Educator>> GetEducatorByCpf(string cpf)
        {
            var educator = await _educatorQueries.GetEducatorByCpf(cpf);
            return educator == null ? NotFound() : CustomResponse(educator);
        }

        [HttpPost("api/educator/create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateEducator([FromBody] RegisterEducatorCommand command)
        {            
            return CustomResponse(await _mediator.SendCommand(command));
        }
        
        [HttpPut("api/educator/update")]
        public async Task<IActionResult> UpdateEducator([FromBody] UpdateEducatorCommand command)
        {
            return CustomResponse(await _mediator.SendCommand(command));
        }
        
        [HttpDelete("api/educator/delete/{id}")]
        public async Task<IActionResult> DeleteEducator([FromBody] DeleteEducatorCommand command)
        {
            return CustomResponse(await _mediator.SendCommand(command));
        }
    }
}
