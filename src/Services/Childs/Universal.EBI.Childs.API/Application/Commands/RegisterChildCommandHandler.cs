using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.Events;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;
using Universal.EBI.Childs.API.Application.Validations;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.DomainObjects.Models.Enums;
using Universal.EBI.Core.Messages;
using Universal.EBI.Core.Messages.Integration.Child;
using Universal.EBI.Core.Utils;
using Universal.EBI.MessageBus.Interfaces;

namespace Universal.EBI.Childs.API.Application.Commands
{
    public class RegisterChildCommandHandler : CommandHandler, IRequestHandler<RegisterChildCommand, ValidationResult>
    {
        private readonly IChildRepository _childRepository;
        private readonly IChildQueries _childQueries;
        private readonly IMessageBus _bus;

        public RegisterChildCommandHandler(IChildRepository childRepository, IChildQueries childQueries, IMessageBus bus)
        {
            _childRepository = childRepository;
            _childQueries = childQueries;
            _bus = bus;
        }

        public async Task<ValidationResult> Handle(RegisterChildCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var child = new Child
            {   
                Id = message.Id,
                FirstName = message.FirstName,
                LastName = message.LastName,
                FullName = $"{message.FirstName} {message.LastName}",
                Email = ValidationUtils.ValidateRequestEmail(message.Email),
                Cpf = ValidationUtils.ValidateRequestCpf(message.Cpf),
                Phones = new List<Phone>(),
                Address = RegisterChildValidation.ValidateRequestAddress(message.Address),
                BirthDate = DateTime.Parse(message.BirthDate).Date,
                GenderType = (GenderType)Enum.Parse(typeof(GenderType), message.Gender, true),
                AgeGroupType = (AgeGroupType)Enum.Parse(typeof(AgeGroupType), message.AgeGroup, true),
                PhotoUrl = message.PhotoUrl,
                Excluded = message.Excluded,
                Responsibles = new List<Responsible>()              
            };            

            var responsilblesLength = message.Responsibles.Length;
            for (int i = 0; i < responsilblesLength; i++)
            {                
                child.Responsibles.Add(new Responsible { Id = message.Responsibles[i].Id });
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
                AddError("Esta criança já está cadastrada.");
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

            //var guidIds = new Guid[message.Responsibles.Length];       
            
            //for (int i = 0; i < message.Responsibles.Length; i++)
            //{
            //    guidIds[i] = message.Responsibles[i].Id;
            //}           

            await _bus.PublishAsync(new RegisteredChildIntegrationEvent { Id = child.Id });            

            return await PersistData(_childRepository.UnitOfWork, success);
        }
    }
}
