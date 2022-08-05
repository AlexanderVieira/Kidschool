using AutoMapper;
using FluentValidation.Results;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.Events;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Commands
{
    public class UpdateChildCommandHandler : CommandHandler, IRequestHandler<UpdateChildCommand, ValidationResult>
    {
        private readonly IChildRepository _childRepository;
        private readonly IChildQueries _childQueries;
        private readonly IMapper _mapper;

        public UpdateChildCommandHandler(IChildRepository childRepository, IChildQueries childQueries, IMapper mapper)
        {
            _childRepository = childRepository;
            _childQueries = childQueries;
            _mapper = mapper;
        }

        public async Task<ValidationResult> Handle(UpdateChildCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var child = _mapper.Map<Child>(message.ChildRequest);
            child.FullName = $"{child.FirstName} {child.LastName}";

            //var existingChild = await _childQueries.GetChildById(child.Id);
            var existingChild = new Child();
            existingChild.Id = child.Id;
            existingChild.FirstName = child.FirstName;
            existingChild.LastName = child.LastName;
            existingChild.FullName = $"{child.FirstName} {child.LastName}";
            existingChild.Email = child.Email; 
            existingChild.Cpf = child.Cpf; 
            existingChild.Phones = child.Phones;
            existingChild.Address = child.Address;
            existingChild.BirthDate = child.BirthDate.Date.ToLocalTime();
            existingChild.GenderType = child.GenderType; 
            existingChild.AgeGroupType = child.AgeGroupType;
            existingChild.PhotoUrl = child.PhotoUrl;
            existingChild.Excluded = child.Excluded;
            existingChild.CreatedDate = DateTime.Parse("2022-07-30");
            existingChild.CreatedBy = "AspNetUser";
            existingChild.LastModifiedBy = child.LastModifiedBy;
            existingChild.LastModifiedDate = DateTime.Now.ToLocalTime();
            //existingChild.Responsibles = child.Responsibles;

            var success = await _childRepository.UpdateChild(existingChild);
            if (!success)
            {
                AddError("Algo deu errado ao tentar atualizar uma criança.");
                return ValidationResult;
            }

            existingChild.AddEvent(new UpdatedChildEvent
            {
                AggregateId = existingChild.Id,
                Id = existingChild.Id,
                FirstName = existingChild.FirstName,
                LastName = existingChild.LastName,
                FullName = existingChild.FullName,
                Email = existingChild.Email?.Address,
                Cpf = existingChild.Cpf?.Number,
                BirthDate = existingChild.BirthDate.ToString(),
                GenderType = existingChild.GenderType.ToString(),
                AgeGroupType = existingChild.AgeGroupType.ToString(),
                PhotoUrl = existingChild.PhotoUrl,
                Excluded = existingChild.Excluded,
                CreatedBy = existingChild.CreatedBy,
                CreatedDate = existingChild.CreatedDate,
                LastModifiedBy = existingChild.LastModifiedBy,
                LastModifiedDate = existingChild.LastModifiedDate,
                Phones = existingChild.Phones.ToArray(),
                Address = existingChild.Address,
                //Responsibles = existingChild.Responsibles.ToArray()
            });

            return await PersistData(_childRepository.UnitOfWork);
        }
    }
}
