using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Responsibles.API.Application.Commands;
using Universal.EBI.Responsibles.API.Application.Queries.Interfaces;
using Universal.EBI.WebAPI.Core.AspNetUser.Interfaces;
using Universal.EBI.WebAPI.Core.Controllers;

namespace Universal.EBI.Responsibles.API.Controllers
{
    public class ResponsibleController : BaseController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IAspNetUser _user;
        private readonly IResponsibleQueries _responsibleQueries;

        public ResponsibleController(IMediatorHandler mediator, IAspNetUser user, IResponsibleQueries responsibleQueries)
        {
            _mediator = mediator;
            _user = user;
            _responsibleQueries = responsibleQueries;
        }

        [HttpGet("api/Responsibles")]
        public async Task<IActionResult> GetResponsibles([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var child = await _responsibleQueries.GetResponsibles(ps, page, q);
            return child == null ? NotFound() : CustomResponse(child);
        }

        [HttpGet("api/Responsible/{id}")]
        public async Task<IActionResult> GetResponsibleById(Guid id)
        {
            var child = await _responsibleQueries.GetResponsibleById(id);
            return child == null ? NotFound() : CustomResponse(child);
        }

        [HttpGet("api/Responsible/{cpf:length(11)}", Name = "GetChildByCpf")]
        public async Task<ActionResult> GetResponsibleByCpf(string cpf)
        {
            var child = await _responsibleQueries.GetResponsibleByCpf(cpf);
            return child == null ? NotFound() : CustomResponse(child);
        }

        [HttpPost("api/Responsible/create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateResponsible([FromBody] RegisterResponsibleCommand command)
        {            
            return CustomResponse(await _mediator.SendCommand(command));
        }
        
        [HttpPut("api/Responsible/update")]
        public async Task<IActionResult> UpdateResponsible([FromBody] UpdateResponsibleCommand command)
        {
            return CustomResponse(await _mediator.SendCommand(command));
        }
        
        [HttpDelete("api/Responsible/delete/{id}")]
        public async Task<IActionResult> DeleteResponsible(Guid id)
        {
            var command = new DeleteResponsibleCommand { Id = id };
            return CustomResponse(await _mediator.SendCommand(command));
        }
    }
}
