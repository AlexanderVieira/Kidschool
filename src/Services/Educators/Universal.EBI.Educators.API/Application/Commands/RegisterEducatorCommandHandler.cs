using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.DomainObjects.Models.Enums;
using Universal.EBI.Core.Messages;
using Universal.EBI.Educators.API.Application.Events;
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
                FullName = message.FullName,
                Email = new Email(message.Email),
                Cpf = new Cpf(message.Cpf),
                Phones = new List<Phone>(),
                Address = new Address(),                 
                BirthDate = DateTime.Parse(message.BirthDate),
                GenderType = (GenderType)Enum.Parse(typeof(GenderType), message.GenderType, true),
                FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), message.FunctionType, true),
                PhotoUrl = message.PhotoUrl,
                Excluded = message.Excluded,
                //CreatedDate = DateTime.Parse(message.CreatedDate),
                //CreatedBy = message.CreatedBy,
                //LastModifiedDate = DateTime.Parse(message.LastModifiedDate),
                //LastModifiedBy = message.LastModifiedBy,
                //ClassroomId = message.ClassroomId
            };
                        
            var phonesCount = message.Phones.Length;
            for (int i = 0; i < phonesCount; i++)
            {               
                educator.Phones.Add(new Phone
                {
                    Id = message.Phones[i].Id,
                    Number = message.Phones[i].Number,
                    PhoneType = message.Phones[i].PhoneType,
                    EducatorId = educator.Id
                });

            }

            educator.Address = new Address
            {
                PublicPlace = message.Address.PublicPlace,
                Number = message.Address.Number,
                Complement = message.Address.Complement,
                District = message.Address.District,
                ZipCode = message.Address.ZipCode,
                City = message.Address.City,
                State = message.Address.State,
                Country = message.Address.Country,
                EducatorId = educator.Id
            };

            var existingCustomer = await _educatorRepository.GetEducatorByCpf(educator.Cpf.Number);

            if (existingCustomer != null)
            {
                AddError("Este CPF já está em uso.");
                return ValidationResult;
            }

            await _educatorRepository.CreateEducator(educator);

            educator.AddEvent(new RegisteredEducatorEvent 
            {
                AggregateId = message.Id,
                Id = message.Id,
                FirstName = message.FirstName,
                LastName = message.LastName,
                FullName = message.FullName,
                Email = message.Email,
                Cpf = message.Cpf,
                Phones = message.Phones,
                Address = message.Address,
                BirthDate = message.BirthDate,
                GenderType = message.GenderType,
                FunctionType = message.FunctionType,
                PhotoUrl = message.PhotoUrl,
                Excluded = message.Excluded,
                //CreatedDate = message.CreatedDate,
                //CreatedBy = message.CreatedBy,
                //LastModifiedDate = message.LastModifiedDate,
                //LastModifiedBy = message.LastModifiedBy,
                //ClassroomId = message.ClassroomId
            });
            
            return await PersistData(_educatorRepository.UnitOfWork);
        }
    }
}
