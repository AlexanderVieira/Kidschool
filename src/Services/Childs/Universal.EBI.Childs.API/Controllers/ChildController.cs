﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Childs.API.Application.Commands;
using Universal.EBI.WebAPI.Core.AspNetUser.Interfaces;
using Universal.EBI.WebAPI.Core.Controllers;
using Universal.EBI.Childs.API.Application.DTOs;
using System.Linq;
using Universal.EBI.Childs.API.Application.Queries;
using Microsoft.AspNetCore.Http;
using FluentValidation.Results;
using Universal.EBI.Childs.API.Application.Validations;

namespace Universal.EBI.Childs.API.Controllers
{
    public class ChildController : BaseController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IAspNetUser _user;

        public ChildController(IMediatorHandler mediator, IAspNetUser user)
        {
            _mediator = mediator;
            _user = user;
        }

        [HttpGet("api/children")]
        public async Task<IActionResult> GetChildrenPaged([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {                
                var response = (GetChildrenPagedQueryResponse)await _mediator.SendQuery(new GetChildrenPagedQuery() 
                { 
                    PageSize = ps,  
                    PageIndex = page, 
                    Query = q 
                });
                return (response.pagedResult == null) || 
                       (response.pagedResult.List == null) || 
                       (response.pagedResult.List.Count() == 0) ? 
                       ProcessingMassage(StatusCodes.Status404NotFound,
                                         "Não existem dados para exibição.") : CustomResponse(response.pagedResult);
            }
            catch (Exception)
            {
                AddProcessingErrors("Sistema indisponível no momento. Tente mais tarde.");
                return CustomResponse();
            }
        }

        [HttpGet("api/children-inactives")]
        public async Task<IActionResult> GetChildrenInactives([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                var response = (GetChildrenPagedInactiveQueryResponse)await _mediator.SendQuery(new GetChildrenPagedInactiveQuery()
                {
                    PageSize = ps,
                    PageIndex = page,
                    Query = q
                });
                return (response.pagedResult == null) ||
                       (response.pagedResult.List == null) ||
                       (response.pagedResult.List.Count() == 0) ?
                       ProcessingMassage(StatusCodes.Status404NotFound,
                                         "Não existem dados para exibição.") : CustomResponse(response.pagedResult);
            }
            catch (Exception)
            {
                AddProcessingErrors("Sistema indisponível no momento. Tente mais tarde.");
                return CustomResponse();
            }
        }

        [HttpGet("api/child/{id:guid}")]
        public async Task<IActionResult> GetChildById(Guid id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {                
                if (!ExcuteValidation(new GetChildByIdQueryValidation(), 
                                      new GetChildByIdQuery { Id = id })) return CustomResponse(ValidationResult);
                var response = (GetChildByIdQueryResponse)await _mediator.SendQuery(new GetChildByIdQuery { Id = id });               
                return response.Child == null ? 
                        ProcessingMassage(StatusCodes.Status404NotFound,
                                          "Não existem dados para exibição.") : CustomResponse(response.Child);
            }
            catch (Exception ex)
            {
                AddProcessingErrors(ex.Message);
                return CustomResponse();
            }
        }

        [HttpGet("api/child/{cpf:length(11)}", Name = "GetChildByCpf")]
        public async Task<ActionResult> GetChildByCpf(string cpf)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {                
                if (!ExcuteValidation(new GetChildByCpfQueryValidation(), 
                                      new GetChildByCpfQuery { Cpf = cpf })) return CustomResponse(ValidationResult);
                var response = (GetChildByCpfQueryResponse)await _mediator.SendQuery(new GetChildByCpfQuery { Cpf = cpf });
                return response.Child == null ? 
                        ProcessingMassage(StatusCodes.Status404NotFound,
                                          "Não existem dados para exibição.") : CustomResponse(response.Child);
            }
            catch (Exception ex)
            {
                AddProcessingErrors(ex.Message);
                return CustomResponse();
            }
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
                request.Responsibles.ToList().ForEach(r => r.CreatedBy = _user.GetUserEmail());
                var command = new RegisterChildCommand(request);                
                ValidationResult = await _mediator.SendCommand(command);
                if (!ValidationResult.IsValid) return CustomResponse(ValidationResult);
                
                AddMessageSuccess("Criança adicionada com sucesso.");                
                return CustomResponse(StatusCodes.Status201Created);
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
                ValidationResult = await _mediator.SendCommand(command);
                if (!ValidationResult.IsValid) return CustomResponse(ValidationResult);

                AddMessageSuccess("Criança atualizada com sucesso.");
                return CustomResponse(StatusCodes.Status200OK);
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
                ValidationResult = await _mediator.SendCommand(new DeleteChildCommand { Id = id });
                if (!ValidationResult.IsValid) return CustomResponse(ValidationResult);

                AddMessageSuccess("Criança excluída com sucesso.");
                return CustomResponse(StatusCodes.Status204NoContent);                
            }
            catch (Exception ex)
            {
                AddProcessingErrors(ex.Message);
                return CustomResponse();
            }
        }

        [HttpPut("api/child/Inactivate")]
        public async Task<IActionResult> InactivateChild([FromBody] ChildRequestDto request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                if (request == null) return CustomResponse();

                request.LastModifiedBy = _user.GetUserEmail();                
                var command = new InactivateChildCommand(request);
                ValidationResult = await _mediator.SendCommand(command);
                if (!ValidationResult.IsValid) return CustomResponse(ValidationResult);

                AddMessageSuccess("Criança inativada com sucesso.");
                return CustomResponse(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                AddProcessingErrors(ex.Message);
                return CustomResponse();
            }
        }

        [HttpPut("api/child/Activate")]
        public async Task<IActionResult> ActivateChild([FromBody] ChildRequestDto request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                if (request == null) return CustomResponse();

                request.LastModifiedBy = _user.GetUserEmail();                
                var command = new ActivateChildCommand(request);
                ValidationResult = await _mediator.SendCommand(command);
                if (!ValidationResult.IsValid) return CustomResponse(ValidationResult);

                AddMessageSuccess("Criança ativada com sucesso.");
                return CustomResponse(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                AddProcessingErrors(ex.Message);
                return CustomResponse();
            }
        }

        [HttpPut("api/child/add/responsible")]
        public async Task<IActionResult> AddResponsible([FromBody] AddResponsibleRequestDto request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            try
            {
                if (request == null) return CustomResponse();

                request.ResponsibleDto.CreatedBy = _user.GetUserEmail();                
                var command = new AddResponsibleCommand(request);
                ValidationResult = await _mediator.SendCommand(command);
                if (!ValidationResult.IsValid) return CustomResponse(ValidationResult);

                AddMessageSuccess("Responsável adicionado com sucesso.");
                return CustomResponse(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                AddProcessingErrors(ex.Message);
                return CustomResponse();
            }
        }

        protected override bool ExcuteValidation<TV, TE>(TV validation, TE entity)
        {
            ValidationResult = validation.Validate(entity);
            if (ValidationResult.IsValid) return true;
            return false;
        }

    }
}
