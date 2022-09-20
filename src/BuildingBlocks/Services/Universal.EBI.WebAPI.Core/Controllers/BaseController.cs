using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using Universal.EBI.Core.Comunication;

namespace Universal.EBI.WebAPI.Core.Controllers
{
    [ApiController]
    public abstract class BaseController : Controller
    {
        protected ICollection<string> Errors = new List<string>();
        protected ValidationResult ValidationResult { get; set; }
        protected string SuccessMessage { get; set; }

        protected virtual bool ExcuteValidation<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : class
        {
            throw new NotImplementedException();
        }
        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
            {
                if (result is int) return MessageHandler((int)result);
                return Ok(result);
            }

            if (result is int) return MessageHandler(result);

            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseResult
            {
                Title = "Opa! Ocorreu um erro.",
                Status = StatusCodes.Status500InternalServerError,
                Errors = new ResponseErrorMessages { Messages = Errors.ToList() }
            });

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
            if (response == null || !response.Errors.Messages.Any()) return false;

            foreach (var message in response.Errors.Messages)
            {
                AddProcessingErrors(message);
            }

            return true;
        }

        protected ActionResult ProcessingMassage(int statusCode, string mensagem)
        {
            AddProcessingErrors(mensagem);
            return CustomResponse(statusCode);
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

        protected void AddMessageSuccess(string message)
        {
            SuccessMessage = message;
        }

        private ActionResult MessageHandler(object status)
        {
            switch (status)
            {
                case 200:
                    return Ok(new ResponseResult
                    {
                        Title = "Opa! Sucesso.",
                        Status = StatusCodes.Status200OK,
                        SuccessMessage = SuccessMessage,
                        Errors = new ResponseErrorMessages { Messages = Errors.ToList() }
                    });                    

                case 201:
                    return StatusCode(StatusCodes.Status201Created, new ResponseResult
                    {
                        Title = "Opa! Sucesso.",
                        Status = StatusCodes.Status201Created,
                        SuccessMessage = SuccessMessage,
                        Errors = new ResponseErrorMessages { Messages = Errors.ToList() }
                    });                   

                case 204:
                    return StatusCode(StatusCodes.Status204NoContent, new ResponseResult
                    {
                        Title = "Opa! Sucesso.",
                        Status = StatusCodes.Status204NoContent,
                        SuccessMessage = SuccessMessage,
                        Errors = new ResponseErrorMessages { Messages = Errors.ToList() }
                    });                   

                case 400:
                    return BadRequest(new ResponseResult
                    {
                        Title = "Opa! Ocorreu um erro.",
                        Status = StatusCodes.Status400BadRequest,
                        Errors = new ResponseErrorMessages { Messages = Errors.ToList() }
                    });

                case 401:
                    return Unauthorized(new ResponseResult
                    {
                        Title = "Opa! Ocorreu um erro.",
                        Status = StatusCodes.Status401Unauthorized,
                        Errors = new ResponseErrorMessages { Messages = Errors.ToList() }
                    });

                case 403:
                    return StatusCode(StatusCodes.Status403Forbidden, new ResponseResult
                    {
                        Title = "Opa! Ocorreu um erro.",
                        Status = StatusCodes.Status403Forbidden,
                        Errors = new ResponseErrorMessages { Messages = Errors.ToList() }
                    });                

                case 404:
                    return NotFound(new ResponseResult
                    {
                        Title = "Opa! Ocorreu um erro.",
                        Status = StatusCodes.Status404NotFound,
                        Errors = new ResponseErrorMessages { Messages = Errors.ToList() }
                    });

                default:
                    return new ObjectResult(new ResponseResult
                    {
                        Title = "Opa! Ocorreu um erro.",
                        Status = StatusCodes.Status500InternalServerError,
                        Errors = new ResponseErrorMessages { Messages = Errors.ToList() }
                    });
                    
            }

        }
    }
}
