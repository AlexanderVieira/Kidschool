using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Childs.API.Application.Commands;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.WebAPI.Core.AspNetUser.Interfaces;
using Universal.EBI.WebAPI.Core.Controllers;

namespace Universal.EBI.Childs.API.Controllers
{
    public class ChildController : BaseController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IAspNetUser _user;
        private readonly IChildQueries _childQueries;

        public ChildController(IMediatorHandler mediator, IAspNetUser user, IChildQueries childQueries)
        {
            _mediator = mediator;
            _user = user;
            _childQueries = childQueries;
        }

        [HttpGet("api/childs")]
        public async Task<IActionResult> GetChilds([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var child = await _childQueries.GetChilds(ps, page, q);
            return child == null ? NotFound() : CustomResponse(child);
        }
        
        [HttpGet("api/child/{id}")]
        public async Task<IActionResult> GetChildById(Guid id)
        {
            var child = await _childQueries.GetChildById(id);
            return child == null ? NotFound() : CustomResponse(child);
        }

        [HttpGet("api/child/{cpf:length(11)}", Name = "GetChildByCpf")]
        public async Task<ActionResult<Child>> GetChildByCpf(string cpf)
        {
            var child = await _childQueries.GetChildByCpf(cpf);
            return child == null ? NotFound() : CustomResponse(child);
        }

        [HttpPost("api/child/create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateChild([FromBody] RegisterChildCommand command)
        {            
            return CustomResponse(await _mediator.SendCommand(command));
        }
        
        [HttpPut("api/child/update")]
        public async Task<IActionResult> UpdateChild([FromBody] UpdateChildCommand command)
        {
            return CustomResponse(await _mediator.SendCommand(command));
        }
        
        [HttpDelete("api/child/delete/{id}")]
        public async Task<IActionResult> DeleteChild([FromBody] DeleteChildCommand command)
        {
            return CustomResponse(await _mediator.SendCommand(command));
        }
    }
}
