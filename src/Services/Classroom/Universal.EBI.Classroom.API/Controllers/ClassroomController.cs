using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Universal.EBI.Classrooms.API.Application.Commands;
using Universal.EBI.Classrooms.API.Application.Queries.Interfaces;
using Universal.EBI.Classrooms.API.Models;
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
            //return CustomResponse();
        }

        [HttpGet("api/classroom/{id}")]
        public async Task<IActionResult> GetClassroomById(Guid id)
        {
            var classroom = await _classroomQueries.GetClassroomById(id);
            return classroom == null ? NotFound() : CustomResponse(classroom);
            //return CustomResponse();
        }        

        [HttpPost("api/classroom/create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateClassroom([FromBody] RegisterClassroomCommand command)
        {               
            return CustomResponse(await _mediator.SendCommand(command));
        }

        [HttpPost("api/classroom/child/add")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddChildsClassroom([FromBody] UpdateClassroomCommand command)
        {
            if (command.Childs.Length == 0)
            {
                return CustomResponse();
            }

            var dictChild = command.Childs.ToDictionary(c => c.Id.ToString());
            IDictionary<string, Child> classroomDictionary = new Dictionary<string, Child>();            
            var childs = new List<Child>();

            if (command.Childs != null)
            {
                for (int i = 0; i < command.Childs.Length; i++)
                {
                    Child childEntry;                    

                    if (!classroomDictionary.TryGetValue(dictChild["Id"].ToString(), out childEntry))
                    {
                        classroomDictionary.Add(childEntry.Id.ToString(), childEntry);
                    }

                    childs.Add(childEntry);
                }

            }

            command.Childs = childs.ToArray();

            return CustomResponse(await _mediator.SendCommand(command));            

        }

        [HttpPut("api/Classroom/update")]
        public async Task<IActionResult> UpdateClassroom([FromBody] UpdateClassroomCommand command)
        {
            return CustomResponse(await _mediator.SendCommand(command));
        }

        [HttpDelete("api/classroom/delete/{id}")]
        public async Task<IActionResult> DeleteClassromm(Guid id)
        {
            var command = new DeleteClassroomCommand { Id = id };
            return CustomResponse(await _mediator.SendCommand(command));
        }
    }
}
