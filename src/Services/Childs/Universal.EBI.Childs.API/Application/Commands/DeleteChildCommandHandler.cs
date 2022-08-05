using AutoMapper;
using FluentValidation.Results;
using MediatR;
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
    public class DeleteChildCommandHandler : CommandHandler, IRequestHandler<DeleteChildCommand, ValidationResult>
    {
        private readonly IChildRepository _childRepository;
        private readonly IChildQueries _childQueries;
        private readonly IMapper _mapper;

        public DeleteChildCommandHandler(IChildRepository childRepository, IChildQueries childQueries, IMapper mapper)
        {
            _childRepository = childRepository;
            _childQueries = childQueries;
            _mapper = mapper;
        }

        public async Task<ValidationResult> Handle(DeleteChildCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;
            var child = _mapper.Map<Child>(message.ChildRequest);
            //var existingChild = await _childQueries.GetChildById(message.Id);
            var existingChild = new Child { Id = child.Id };

            if (existingChild == null)
            {
                AddError("Criança não encontrado.");
                return ValidationResult;
            }

            var success = await _childRepository.DeleteChild(child);
            if (success)
            {
                child.AddEvent(new DeletedChildEvent
                {
                    AggregateId = child.Id,
                    Id = child.Id,                    
                    FirstName = child.FirstName,
                    LastName = child.LastName,
                    FullName = child.FullName,
                    Email = child.Email?.Address,
                    Cpf = child.Cpf?.Number,
                    BirthDate = child.BirthDate.ToString(),
                    GenderType = child.GenderType.ToString(),
                    AgeGroupType = child.AgeGroupType.ToString(),
                    PhotoUrl = child.PhotoUrl,
                    Excluded = child.Excluded,
                    CreatedBy = child.CreatedBy,
                    CreatedDate = child.CreatedDate,
                    LastModifiedBy = child.LastModifiedBy,
                    LastModifiedDate = child.LastModifiedDate,
                    Phones = child.Phones.ToArray(),
                    Address = child.Address,
                    Responsibles = child.Responsibles.ToArray()
                });
            }

            return await PersistData(_childRepository.UnitOfWork);
        }
    }
}
