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
using Universal.EBI.Childs.API.Application.DTOs;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Universal.EBI.Childs.API.Application.Queries;
using Microsoft.AspNetCore.Http;

namespace Universal.EBI.Childs.API.Controllers
{
    public class ChildController : BaseController
    {
        private readonly IMediatorHandler _mediator;        
        private readonly IChildQueries _childQueries;
        private readonly IAspNetUser _user;
        private readonly IMapper _mapper;

        public ChildController(IMediatorHandler mediator, 
                               IChildQueries childQueries, 
                               IAspNetUser user, 
                               IMapper mapper)
        {
            _mediator = mediator;
            _childQueries = childQueries;
            _user = user;
            _mapper = mapper;
        }

        [HttpGet("api/children/name")]
        public async Task<IActionResult> GetChildrenPaged([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            try
            {
                var response = (GetChildrenPagedQueryResponse)await _mediator.SendQuery(new GetChildrenPagedQuery() 
                { 
                    PageSize = ps,  
                    PageIndex = page, 
                    Query = q 
                });
                return response.pagedResult == null ? ProcessingMassage(StatusCodes.Status404NotFound, 
                                                                        "Não existem dados para exibição.") : CustomResponse(response.pagedResult);
            }
            catch (Exception ex)
            {
                AddProcessingErrors(ex.Message);
                return CustomResponse();
            }
        }

        [HttpGet("api/child/{id}")]
        public async Task<IActionResult> GetChildById(Guid id)
        {
            var child = await _childQueries.GetChildById(id);
            return child == null ? NotFound() : CustomResponse(child);
        }

        [HttpGet("api/child/{cpf:length(11)}", Name = "GetChildByCpf")]
        public async Task<ActionResult> GetChildByCpf(string cpf)
        {
            var child = await _childQueries.GetChildByCpf(cpf);
            return child == null ? NotFound() : CustomResponse(child);
        }

        [HttpPost("api/child/create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateChild([FromBody] ChildRequestDto request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                if (request == null) return CustomResponse();
                request.FullName = $"{request.FirstName} {request.LastName}";
                request.CreatedBy = _user.GetUserEmail();
                request.Responsibles.ToList().ForEach(r => r.CreatedBy = _user.GetUserEmail());
                request.Responsibles.ToList().ForEach(r => r.FullName = $"{r.FirstName} {r.LastName}");
                var command = new RegisterChildCommand(request);
                return CustomResponse(await _mediator.SendCommand(command));
            }
            catch (Exception ex)
            {
                AddProcessingErrors(ex.Message);
                return CustomResponse();
            }
            
        }
        
        [HttpPut("api/child/update")]
        public async Task<IActionResult> UpdateChild([FromBody] ChildRequestDto request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                if (request == null) return CustomResponse();                
                request.LastModifiedBy = _user.GetUserEmail();
                request.Responsibles.ToList().ForEach(r => r.LastModifiedBy = _user.GetUserEmail());
                var command = new UpdateChildCommand(request);
                return CustomResponse(await _mediator.SendCommand(command));
            }
            catch (Exception ex)
            {
                AddProcessingErrors(ex.Message);
                return CustomResponse();
            }
        }
        
        [HttpDelete("api/child/delete/{id}")]
        public async Task<IActionResult> DeleteChild(Guid id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {                              
                return CustomResponse(await _mediator.SendCommand(new DeleteChildCommand { Id = id }));
            }
            catch (Exception ex)
            {
                AddProcessingErrors(ex.Message);
                return CustomResponse();
            }
        }
       
    }
}
