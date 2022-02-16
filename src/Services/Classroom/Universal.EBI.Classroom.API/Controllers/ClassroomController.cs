﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Universal.EBI.Classrooms.API.Application.Commands;
using Universal.EBI.Classrooms.API.Application.Queries.Interfaces;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.WebAPI.Core.AspNetUser.Interfaces;
using Universal.EBI.WebAPI.Core.Controllers;

namespace Universal.EBI.Classrooms.API.Controllers
{
    public class ClassroomController : BaseController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IAspNetUser _user;
        private readonly IClassroomQueries _classroomQueries;       

        public ClassroomController(IMediatorHandler mediator, IAspNetUser user, IClassroomQueries classroomQueries)
        {
            _mediator = mediator;
            _user = user;
            _classroomQueries = classroomQueries;
        }        

        [HttpGet("api/classrooms")]
        public async Task<IActionResult> GetClassrooms([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var classroom = await _classroomQueries.GetClassrooms(ps, page, q);
            return classroom == null ? NotFound() : CustomResponse(classroom);            
        }

        [HttpGet("api/classroom/{id}")]
        public async Task<IActionResult> GetClassroomById(Guid id)
        {
            var classroom = await _classroomQueries.GetClassroomById(id);
            return classroom == null ? NotFound() : CustomResponse(classroom);            
        }        

        [HttpPost("api/classroom/create")]        
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateClassroom([FromBody] RegisterClassroomCommand command)
        {
            //command.CreatedDate = DateTime.Now.ToString();
            //command.CreatedBy = "Admin";
            return CustomResponse(await _mediator.SendCommand(command));
        }        

        [HttpPut("api/Classroom/update")]
        public async Task<IActionResult> UpdateClassroom([FromBody] UpdateClassroomCommand command)
        {
            command.LastModifiedDate = DateTime.UtcNow.ToShortDateString();
            command.LastModifiedBy = "Admin";
            return CustomResponse(await _mediator.SendCommand(command));
        }

        [HttpDelete("api/classroom/delete/{id}")]
        public async Task<IActionResult> DeleteClassromm(Guid id)
        {
            var command = new DeleteClassroomCommand 
            {
                Id = id,
                LastModifiedDate = DateTime.UtcNow.ToShortDateString(),
                LastModifiedBy = _user.Name
            };
            return CustomResponse(await _mediator.SendCommand(command));
        }

        [HttpPost("api/classroom/child/add")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddChildsClassroom([FromBody] UpdateClassroomCommand command)
        {
            if (command.Childs == null || command.Childs.Length <= 0)
            {
                return CustomResponse(command);
            }

            command.LastModifiedDate = DateTime.UtcNow.ToShortDateString();
            command.LastModifiedBy = _user.Name;

            return CustomResponse(await _mediator.SendCommand(command));                   

        }

        [HttpPost("api/classroom/child/delete")]
        public async Task<IActionResult> DeleteChildsClassromm([FromBody] DeleteChildClassroomCommand command)
        {
            command.LastModifiedDate = DateTime.UtcNow.ToShortDateString();
            command.LastModifiedBy = _user.Name;
            return CustomResponse(await _mediator.SendCommand(command));
        }
    }
}