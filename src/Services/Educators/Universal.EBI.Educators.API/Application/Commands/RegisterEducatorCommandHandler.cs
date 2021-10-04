﻿using FluentValidation.Results;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.Messages;
using Universal.EBI.Educators.API.Application.Events;
using Universal.EBI.Educators.API.Models;
using Universal.EBI.Educators.API.Models.Interfaces;

namespace Universal.EBI.Educators.API.Application.Commands
{
    public class RegisterEducatorCommandHandler : CommandHandler, IRequestHandler<RegisterEducatorCommand, ValidationResult>
    {
        private readonly IEducatorRepository _educatorRepository;

        public RegisterEducatorCommandHandler(IEducatorRepository educatorRepository)
        {
            _educatorRepository = educatorRepository;
        }

        public async Task<ValidationResult> Handle(RegisterEducatorCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;
                        
            var educator = new Educator
            {
                Id = message.Id,
                FirstName = message.FirstName,
                LastName = message.LastName,
                Email = new Email(message.Email),
                Cpf = new Cpf(message.Cpf),
                Phones = message.Phones,
                Address = message.Address,
                BirthDate = DateTime.Parse(message.BirthDate),
                Gender = (Gender)Enum.Parse(typeof(Gender), message.Gender, true),
                FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), message.Function, true),
                PhotoUrl = message.PhotoUrl,
                Excluded = message.Excluded
            };
                        
            var phonesCount = educator.Phones.Count;
            for (int i = 0; i < phonesCount; i++)
            {
                educator.Phones.ToList()[i].EducatorId = educator.Id;                               
            }

            educator.Address = new Address(educator.Address.PublicPlace,
                                           educator.Address.Number,
                                           educator.Address.Complement,
                                           educator.Address.District,
                                           educator.Address.ZipCode,
                                           educator.Address.City,
                                           educator.Address.State,
                                           educator.Id);

            var existingCustomer = await _educatorRepository.GetEducatorByCpf(educator.Cpf.Number);

            if (existingCustomer != null)
            {
                AddError("Este CPF já está em uso.");
                return ValidationResult;
            }

            await _educatorRepository.CreateEducator(educator);

            educator.AddEvent(new RegisteredEducatorEvent 
            {                
                Id = message.Id,
                FirstName = message.FirstName,
                LastName = message.LastName,
                Email = message.Email,
                Cpf = message.Cpf,
                Phones = message.Phones,
                Address = message.Address,
                BirthDate = message.BirthDate,
                Gender = message.Gender,
                Function = message.Function,
                PhotoUrl = message.PhotoUrl,
                Excluded = message.Excluded
            });
            
            return await PersistData(_educatorRepository.UnitOfWork);
        }
    }
}