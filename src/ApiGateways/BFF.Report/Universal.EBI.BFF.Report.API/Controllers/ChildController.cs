using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
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
        [Route("api/bff/children")]
        public async Task<ActionResult> GetChildren([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                var response = await _childService.GetChildren(ps, page, q);
                return response;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        [HttpGet("api/bff/children-inactives")]
        public async Task<ActionResult> GetChildrenInactives([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                var response = await _childService.GetChildrenInactives(ps, page, q);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("api/bff/child/{id}")] 
        public async Task<ActionResult> GetchildById(Guid id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                var response = await _childService.GetChildById(id);
                return response;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        [HttpGet("api/bff/child/{cpf:length(11)}")]
        public async Task<ActionResult<ChildResponseDto>> GetchildByCpf(string cpf)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                var response = await _childService.GetChildByCpf(cpf);
                return response;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        [HttpPost("api/bff/child/create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateChild([FromBody] ChildRequestDto request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                var response = await _childService.CreateChild(request);
                return response;
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPut("api/bff/child/update")]
        public async Task<ActionResult> UpdateChild([FromBody] ChildRequestDto request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                var response = await _childService.UpdateChild(request);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("api/bff/child/delete/{id}")]
        public async Task<ActionResult> DeleteChild(Guid id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                var response = await _childService.DeleteChild(id);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("api/bff/child/inactivate")]
        public async Task<ActionResult> InactivateChild([FromBody] ChildRequestDto request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                var response = await _childService.InactivateChild(request);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("api/bff/child/activate")]
        public async Task<IActionResult> ActivateChild([FromBody] ChildRequestDto request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                var response = await _childService.ActivateChild(request);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("api/bff/child/add/responsible")]
        public async Task<IActionResult> AddResponsible([FromBody] AddResponsibleRequestDto request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                var response = await _childService.AddResponsible(request);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPut("api/bff/child/delete/responsible")]
        public async Task<IActionResult> DeleteResponsible([FromBody] DeleteResponsibleRequestDto request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                var response = await _childService.DeleteResponsible(request);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
