using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.Events;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.Messages;
using Universal.EBI.Core.Utils;

namespace Universal.EBI.Childs.API.Application.Commands
{
    public class UpdateChildCommandHandler : CommandHandler, IRequestHandler<UpdateChildCommand, ValidationResult>
    {
        private readonly IChildRepository _childRepository;
        private readonly IChildQueries _childQueries;

        public UpdateChildCommandHandler(IChildRepository childRepository, IChildQueries childQueries)
        {
            _childRepository = childRepository;
            _childQueries = childQueries;
        }

        public async Task<ValidationResult> Handle(UpdateChildCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var existingChild = await _childQueries.GetChildByCpf(message.Cpf);

            existingChild.FirstName = message.FirstName;
            existingChild.LastName = message.LastName;
            existingChild.FullName = $"{message.FirstName} {message.LastName}";
            existingChild.Email = ValidationUtils.ValidateRequestEmail(message.Email);
            existingChild.Cpf = ValidationUtils.ValidateRequestCpf(message.Email);
            existingChild.Phones = message.Phones;
            existingChild.Address = message.Address;
            existingChild.BirthDate = DateTime.Parse(message.BirthDate);
            existingChild.GenderType = (GenderType)Enum.Parse(typeof(GenderType), message.Gender, true);
            existingChild.AgeGroupType = (AgeGroupType)Enum.Parse(typeof(AgeGroupType), message.AgeGroup, true);
            existingChild.PhotoUrl = message.PhotoUrl;
            existingChild.Excluded = message.Excluded;
            existingChild.Responsibles = message.Responsibles;

            var success = await _childRepository.UpdateChild(existingChild);

            existingChild.AddEvent(new UpdatedChildEvent
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
                AgeGroup = message.AgeGroup,
                PhotoUrl = message.PhotoUrl,
                Excluded = message.Excluded,
                Responsibles = message.Responsibles
            });

            return await PersistData(_childRepository.UnitOfWork, success);
        }
    }
}
