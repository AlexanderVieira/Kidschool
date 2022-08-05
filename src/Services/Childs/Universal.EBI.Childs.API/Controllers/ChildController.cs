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

        [HttpGet("api/childs")]
        public async Task<PagedResult<ChildResponseDto>> GetChilds([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var pagedResult = await _childQueries.GetChilds(ps, page, q);
            
            var childrenDto = new List<ChildResponseDto>();            
            var pagedResultDto = new PagedResult<ChildResponseDto>
            {
                List = new List<ChildResponseDto>(),
                PageIndex = pagedResult.PageIndex,
                PageSize = pagedResult.PageSize,
                Query = pagedResult.Query,
                TotalResults = pagedResult.TotalResults
            };

            foreach (var child in pagedResult.List)
            {
                var childResponse = _mapper.Map<ChildResponseDto>(child);
                childrenDto.Add(childResponse);
            }           

            pagedResultDto.List = childrenDto;
            return pagedResultDto;
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
                request.CreatedBy = _user.GetUserEmail();
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
                var command = new UpdateChildCommand(request);
                return CustomResponse(await _mediator.SendCommand(command));
            }
            catch (Exception ex)
            {
                AddProcessingErrors(ex.Message);
                return CustomResponse();
            }
        }
        
        [HttpDelete("api/child/delete")]
        public async Task<IActionResult> DeleteChild([FromBody] ChildRequestDto request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                if (request == null) return CustomResponse();
                //request.LastModifiedBy = _user.GetUserEmail();
                var command = new DeleteChildCommand(request);
                return CustomResponse(await _mediator.SendCommand(command));
            }
            catch (Exception ex)
            {
                AddProcessingErrors(ex.Message);
                return CustomResponse();
            }
        }
       
    }
}
