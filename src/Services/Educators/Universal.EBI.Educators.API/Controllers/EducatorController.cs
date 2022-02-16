﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Educators.API.Application.Commands;
using Universal.EBI.Educators.API.Application.Dtos;
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
        public async Task<PagedResult<EducatorClassroomDto>> GetEducators([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var pagedResult = await _educatorQueries.GetEducators(ps, page, q);            
            var educatorClassrooms = new List<EducatorClassroomDto>();

            var pagedResultDto = new PagedResult<EducatorClassroomDto>
            {
                List = new List<EducatorClassroomDto>(),
                PageIndex = pagedResult.PageIndex,
                PageSize = pagedResult.PageSize,
                Query = pagedResult.Query,
                TotalResults = pagedResult.TotalResults
            };

            for (int i = 0; i < pagedResult.List.ToList().Count; i++)
            {
                var item = pagedResult.List.ToList()[i];
                //pagedResultDto.List.ToList().Add(
                var educatorClassroomDto = new EducatorClassroomDto
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    BirthDate = item.BirthDate.Date.ToShortDateString(),
                    GenderType = item.GenderType.ToString(),
                    FunctionType = item.FunctionType.ToString()
                };

                educatorClassrooms.Add(educatorClassroomDto);

            }

            pagedResultDto.List = educatorClassrooms;
            return pagedResultDto;

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
        public async Task<IActionResult> DeleteEducator(Guid id)
        {
            var command = new DeleteEducatorCommand { Id = id };
            return CustomResponse(await _mediator.SendCommand(command));
        }
    }
}