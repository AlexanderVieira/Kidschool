﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Universal.EBI.BFF.Report.API.Models;
using Universal.EBI.BFF.Report.API.Services.Interfaces;
using Universal.EBI.WebAPI.Core.Controllers;

namespace Universal.EBI.BFF.Report.API.Controllers
{
    public class ClassroomController : BaseController
    {
        private readonly IClassroomService _classroomService;

        public ClassroomController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }        

        [HttpGet]
        [Route("reports/classrooms")]
        public async Task<PagedResult<ClassroomDto>> GetClassrooms([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            return await _classroomService.GetClassrooms(ps, page, q);
        }

        [HttpGet]
        [Route("reports/classroom/{id}")]
        public async Task<IActionResult> GetClassroomById(Guid id)
        {
            var classroom = await _classroomService.GetClassroomById(id);
            return classroom == null ? NotFound() : CustomResponse(classroom);
        }

        [HttpPost]
        [Route("reports/classroom/create")]
        public async Task<IActionResult> CreateClassroom([FromBody] ClassroomDto classroomDto)
        {
            var response = await _classroomService.CreateClassroom(classroomDto);
            return CustomResponse(response);
        }

        [HttpPut]
        [Route("reports/classroom/update")]
        public async Task<IActionResult> UpdateClassroom([FromBody] ClassroomDto classroomDto)
        {
            var response = await _classroomService.UpdateClassroom(classroomDto);
            return CustomResponse(response);
        }

        [HttpPost]
        [Route("reports/classroom/child/add")]
        public async Task<IActionResult> AddChildsClassroom([FromBody] ClassroomDto classroomDto)
        {
            var response = await _classroomService.AddChildsClassroom(classroomDto);
            return CustomResponse(response);
        }

        [HttpDelete]
        [Route("reports/classroom/delete/{id}")]
        public async Task<IActionResult> DeleteClassroom(Guid id)
        {
            var response = await _classroomService.DeleteClassroom(id);
            return CustomResponse(response);
        }

        [HttpPost]
        [Route("reports/classroom/child/delete")]
        public async Task<IActionResult> DeleteChildsClassroom([FromBody] DeleteChildClassroomDto deleteChildClassroomDto)
        {
            var response = await _classroomService.DeleteChildsClassroom(deleteChildClassroomDto);
            return CustomResponse(response);
        }
    }
}