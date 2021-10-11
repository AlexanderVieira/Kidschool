using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Responsibles.API.Application.Events;
using Universal.EBI.Responsibles.API.Application.Queries.Interfaces;
using Universal.EBI.Responsibles.API.Models;
using Universal.EBI.Responsibles.API.Models.Interfaces;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.Messages;
using Universal.EBI.Core.Utils;

namespace Universal.EBI.Responsibles.API.Application.Commands
{
    public class UpdateResponsibleCommandHandler : CommandHandler, IRequestHandler<UpdateResponsibleCommand, ValidationResult>
    {
        private readonly IResponsibleRepository _responsibleRepository;
        private readonly IResponsibleQueries _responsibleQueries;

        public UpdateResponsibleCommandHandler(IResponsibleRepository childRepository, IResponsibleQueries childQueries)
        {
            _responsibleRepository = childRepository;
            _responsibleQueries = childQueries;
        }

        public async Task<ValidationResult> Handle(UpdateResponsibleCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var existingChild = await _responsibleQueries.GetResponsibleByCpf(message.Cpf);

            existingChild.FirstName = message.FirstName;
            existingChild.LastName = message.LastName;
            existingChild.FullName = $"{message.FirstName} {message.LastName}";
            existingChild.Email = ValidationUtils.ValidateRequestEmail(message.Email);
            existingChild.Cpf = ValidationUtils.ValidateRequestCpf(message.Cpf);
            existingChild.Phones = message.Phones;
            existingChild.Address = message.Address;
            existingChild.BirthDate = DateTime.Parse(message.BirthDate);
            existingChild.GenderType = (GenderType)Enum.Parse(typeof(GenderType), message.Gender, true);
            existingChild.KinshipType = (KinshipType)Enum.Parse(typeof(KinshipType), message.Kinship, true);
            existingChild.PhotoUrl = message.PhotoUrl;
            existingChild.Excluded = message.Excluded;
            existingChild.Childs = message.Childs;

            var success = await _responsibleRepository.UpdateResponsible(existingChild);

            existingChild.AddEvent(new UpdatedResponsibleEvent
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
                Gender = message.Gender,
                Kinship = message.Kinship,
                PhotoUrl = message.PhotoUrl,
                Excluded = message.Excluded,
                Childs = message.Childs
            });

            return await PersistData(_responsibleRepository.UnitOfWork, success);
        }
    }
}
