﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Universal.EBI.Auth.API.Models;
using Universal.EBI.Auth.API.Services;
using Universal.EBI.Core.Integration.Educator;
using Universal.EBI.Core.Messages;
using Universal.EBI.Core.Messages.Integration;
using Universal.EBI.MessageBus.Interfaces;
using Universal.EBI.WebAPI.Core.Controllers;

namespace Universal.EBI.Auth.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly AuthService _authenticationService;
        private readonly IMessageBus _bus;

        //public AuthController(AuthService authenticationService)
        //{
        //    _authenticationService = authenticationService;            
        //}

        public AuthController(AuthService authenticationService, IMessageBus bus)
        {
            _authenticationService = authenticationService;
            _bus = bus;
        }

        [HttpPost("signup")]
        public async Task<ActionResult> Register(UserRegister userRegister)
        {

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = true
            };

            var result = await _authenticationService.UserManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                var educatorResult = await EducatorRecord(userRegister);
                if (!educatorResult.ValidationResult.IsValid)
                {
                    await _authenticationService.UserManager.DeleteAsync(user);
                    return CustomResponse(educatorResult.ValidationResult);
                }
                return CustomResponse(await _authenticationService.GenerateJwt(userRegister.Email));
            }

            foreach (var error in result.Errors)
            {
                AddProcessingErrors(error.Description);
            }

            return CustomResponse();
        }

        [HttpPost("singin")]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _authenticationService.SignInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await _authenticationService.GenerateJwt(userLogin.Email));
            }

            if (result.IsLockedOut)
            {
                AddProcessingErrors("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }

            AddProcessingErrors("Usuário ou Senha incorretos");
            return CustomResponse();
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                AddProcessingErrors("Refresh Token inválido");
                return CustomResponse();
            }

            var token = await _authenticationService.GetRefreshToken(Guid.Parse(refreshToken));

            if (token is null)
            {
                AddProcessingErrors("Refresh Token expirado");
                return CustomResponse();
            }

            return CustomResponse(await _authenticationService.GenerateJwt(token.Username));
        }

        private async Task<ResponseMessage> EducatorRecord(UserRegister userRegister)
        {
            var user = await _authenticationService.UserManager.FindByEmailAsync(userRegister.Email);
            var registeredUser = new RegisteredEducatorIntegrationBaseEvent
            {
                AggregateId = Guid.Parse(user.Id),
                Id = Guid.Parse(user.Id),
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                Email = userRegister.Email,
                Cpf = userRegister.Cpf,
                BirthDate = userRegister.BirthDate,
                Function = userRegister.Function,
                Gender = userRegister.Gender,
                PhotoUrl = userRegister.PhotoUrl,
                Excluded = userRegister.Excluded
            };

            try
            {
                return await _bus.RequestAsync<RegisteredEducatorIntegrationBaseEvent, ResponseMessage>(registeredUser);
            }
            catch
            {
                await _authenticationService.UserManager.DeleteAsync(user);
                throw;
            }
        }
    }
}
