﻿using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using Universal.EBI.Core.Comunication;

namespace Universal.EBI.WebAPI.Core.Controllers
{
    [ApiController]
    public abstract class BaseController : Controller
    {
        protected ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", Errors.ToArray() }
            }));

        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddProcessingErrors(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                AddProcessingErrors(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ResponseResult response)
        {
            ResponseHasErrors(response);

            return CustomResponse();
        }

        protected bool ResponseHasErrors(ResponseResult response)
        {
            if (response == null || !response.Errors.Mensagens.Any()) return false;

            foreach (var message in response.Errors.Mensagens)
            {
                AddProcessingErrors(message);
            }

            return true;
        }

        protected bool ValidOperation()
        {
            return !Errors.Any();
        }

        protected void AddProcessingErrors(string error)
        {
            Errors.Add(error);
        }

        protected void ClearProcessingErrors()
        {
            Errors.Clear();
        }
    }
}
