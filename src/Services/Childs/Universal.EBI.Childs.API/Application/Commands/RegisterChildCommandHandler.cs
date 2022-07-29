using AutoMapper;
using FluentValidation.Results;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.Events;
using Universal.EBI.Childs.API.Application.Queries.Interfaces;
using Universal.EBI.Childs.API.Models.Interfaces;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Commands
{
    public class RegisterChildCommandHandler : CommandHandler, IRequestHandler<RegisterChildCommand, ValidationResult>
    {
        private readonly IChildRepository _childRepository;
        private readonly IChildQueries _childQueries;
        //private readonly IMessageBus _bus;
        private readonly IMapper _mapper;

        public RegisterChildCommandHandler(IChildRepository childRepository, 
                                           IChildQueries childQueries,                                           
                                           IMapper mapper)
        {
            _childRepository = childRepository;
            _childQueries = childQueries;
            //_bus = bus;
            _mapper = mapper;
        }

        public async Task<ValidationResult> Handle(RegisterChildCommand request, CancellationToken cancellationToken)
        {
            var message = request.ChildRequest;
            if (!request.IsValid()) return request.ValidationResult;

            var child = _mapper.Map<Child>(message);           
           
            var existingChild = await _childQueries.GetChildById(child.Id);
            if (existingChild != null)
            {
                AddError("Esta criança já está cadastrada.");
                return ValidationResult;
            }

            await _childRepository.CreateChild(child);
            //var createdChild = await _childQueries.GetChildById(child.Id);
            //var success = createdChild != null;            

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

            //await _bus.PublishAsync(new RegisteredChildIntegrationEvent { Id = child.Id });            

            return await PersistData(_childRepository.UnitOfWork);
        }
    }
}
