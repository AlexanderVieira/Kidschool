using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.Events;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Core.Messages;
using Universal.EBI.Core.Utils;

namespace Universal.EBI.Childs.API.Application.Commands
{
    public class RegisterChildCommandHandler : CommandHandler, IRequestHandler<RegisterChildCommand, ValidationResult>
    {
        private readonly IChildRepository _childRepository;
        private readonly IChildQueries _childQueries;

        public RegisterChildCommandHandler(IChildRepository childRepository, IChildQueries childQueries)
        {
            _childRepository = childRepository;
            _childQueries = childQueries;
        }

        public async Task<ValidationResult> Handle(RegisterChildCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var child = new Child
            {                
                FirstName = message.FirstName,
                LastName = message.LastName,
                FullName = $"{message.FirstName} {message.LastName}",
                Email = ValidationUtils.ValidateRequestEmail(message.Email),
                Cpf = ValidationUtils.ValidateRequestCpf(message.Email),
                Phones = new List<Phone>(),
                Address = new Address 
                { 
                    PublicPlace = message.Address.PublicPlace, 
                    Number = message.Address.Number, 
                    Complement = message.Address.Complement, 
                    District = message.Address.District,
                    City = message.Address.City,
                    State = message.Address.State,
                    ZipCode = message.Address.ZipCode                  
                },
                BirthDate = DateTime.Parse(message.BirthDate).Date,
                GenderType = (GenderType)Enum.Parse(typeof(GenderType), message.Gender, true),
                AgeGroupType = (AgeGroupType)Enum.Parse(typeof(AgeGroupType), message.AgeGroup, true),
                PhotoUrl = message.PhotoUrl,
                Excluded = message.Excluded,
                Responsibles = new List<Responsible>()              
            };

            child.Address.ChildId = child.Id;

            var responsilblesLength = message.Responsibles.Length;
            for (int i = 0; i < responsilblesLength; i++)
            {                
                child.Responsibles.Add(new Responsible { ChildId = child.Id });
            }

            var phonesLength = message.Phones.Length;
            for (int i = 0; i < phonesLength; i++)
            {                
                child.Phones.Add(new Phone 
                { 
                    Number = message.Phones[i].Number, 
                    PhoneType = message.Phones[i].PhoneType, 
                    ChildId = child.Id 
                });
            }           

            var existingChild = await _childQueries.GetChildById(child.Id);

            if (existingChild != null)
            {
                AddError("Este CPF já está em uso.");
                return ValidationResult;
            }

            await _childRepository.CreateChild(child);
            var createdChild = await _childQueries.GetChildById(child.Id);
            var success = createdChild != null;            

            child.AddEvent(new RegisteredChildEvent 
            { 
                AggregateId = child.Id,
                Id = child.Id,
                FirstName = child.FirstName,
                LastName = child.LastName,
                FullName = child.FullName,
                Email = child.Email?.Address,
                Cpf = child.Cpf?.Number,
                Phones = child.Phones.ToArray(),
                Address = child.Address,
                BirthDate = child.BirthDate.ToString(),
                Gender = child.GenderType.ToString(),
                AgeGroup = child.AgeGroupType.ToString(),
                PhotoUrl = child.PhotoUrl,
                Excluded = child.Excluded,
                Responsibles = child.Responsibles.ToArray()
            });
            
            return await PersistData(_childRepository.UnitOfWork, success);
        }
    }
}
