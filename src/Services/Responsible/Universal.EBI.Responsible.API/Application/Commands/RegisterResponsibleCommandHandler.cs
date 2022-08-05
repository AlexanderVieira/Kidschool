using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Responsibles.API.Application.Events;
using Universal.EBI.Responsibles.API.Application.Queries.Interfaces;
using Universal.EBI.Responsibles.API.Models.Interfaces;
using Universal.EBI.Core.Messages;
using Universal.EBI.Core.Utils;
using Universal.EBI.MessageBus.Interfaces;
using Universal.EBI.Core.Messages.Integration.Responsible;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Responsibles.API.Application.Commands
{
    public class RegisterResponsibleCommandHandler : CommandHandler, IRequestHandler<RegisterResponsibleCommand, ValidationResult>
    {
        private readonly IResponsibleRepository _responsibleRepository;
        private readonly IResponsibleQueries _responsibleQueries;
        private readonly IMessageBus _bus;

        public RegisterResponsibleCommandHandler(IResponsibleRepository responsibleRepository, IResponsibleQueries responsibleQueries, IMessageBus bus)
        {
            _responsibleRepository = responsibleRepository;
            _responsibleQueries = responsibleQueries;
            _bus = bus;
        }

        public async Task<ValidationResult> Handle(RegisterResponsibleCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var responsible = new Responsible
            {
                Id = message.Id,
                FirstName = message.FirstName,
                LastName = message.LastName,
                FullName = $"{message.FirstName} {message.LastName}",
                Email = ValidationUtils.ValidateRequestEmail(message.Email),
                Cpf = ValidationUtils.ValidateRequestCpf(message.Cpf),
                Phones = new List<Phone>(),
                Address = new Address
                { 
                    PublicPlace = message.Address.PublicPlace, 
                    Number = message.Address.Number, 
                    Complement = message.Address.Complement, 
                    District = message.Address.District,
                    City = message.Address.City,
                    State = message.Address.State,
                    ZipCode = message.Address.ZipCode,
                    ResponsibleId = message.Id
                },
                BirthDate = DateTime.Parse(message.BirthDate).Date,
                GenderType = (GenderType)Enum.Parse(typeof(GenderType), message.Gender, true),
                KinshipType = (KinshipType)Enum.Parse(typeof(KinshipType), message.Kinship, true),
                PhotoUrl = message.PhotoUrl,
                Excluded = message.Excluded,
                Children = new List<Child>()              
            };

            responsible.Address.ResponsibleId = responsible.Id;

            var childsLength = message.Childs.Length;
            for (int i = 0; i < childsLength; i++)
            {                
                responsible.Children.Add(new Child {  });
            }

            var phonesLength = message.Phones.Length;
            for (int i = 0; i < phonesLength; i++)
            {                
                responsible.Phones.Add(new Phone 
                { 
                    Number = message.Phones[i].Number, 
                    PhoneType = message.Phones[i].PhoneType, 
                    ResponsibleId = responsible.Id 
                });
            }           

            var existingResponsible = await _responsibleQueries.GetResponsibleById(responsible.Id);

            if (existingResponsible != null)
            {
                AddError("Este CPF já está em uso.");
                return ValidationResult;
            }

            await _responsibleRepository.CreateResponsible(responsible);
            var createdresponsible = await _responsibleQueries.GetResponsibleById(responsible.Id);
            var success = createdresponsible != null;            

            responsible.AddEvent(new RegisteredResponsibleEvent 
            { 
                AggregateId = responsible.Id,
                Id = responsible.Id,
                FirstName = responsible.FirstName,
                LastName = responsible.LastName,
                FullName = responsible.FullName,
                Email = responsible.Email?.Address,
                Cpf = responsible.Cpf?.Number,
                Phones = responsible.Phones.ToArray(),
                Address = responsible.Address,
                BirthDate = responsible.BirthDate.ToString(),
                Gender = responsible.GenderType.ToString(),
                Kinship = responsible.KinshipType.ToString(),
                PhotoUrl = responsible.PhotoUrl,
                Excluded = responsible.Excluded,
                Childs = responsible.Children.ToArray()
            });

            //var guidIds = new Guid[message.Childs.Length];

            //for (int i = 0; i < message.Childs.Length; i++)
            //{
            //    guidIds[i] = message.Childs[i].Id;
            //}

            await _bus.PublishAsync(new RegisteredResponsibleIntegrationEvent { Id = responsible.Id });            

            return await PersistData(_responsibleRepository.UnitOfWork);
        }
    }
}
