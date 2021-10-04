using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.Events;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Commands
{
    public class UpdateChildCommandHandler : CommandHandler, IRequestHandler<UpdateChildCommand, ValidationResult>
    {
        private readonly IChildRepository _ChildRepository;

        public UpdateChildCommandHandler(IChildRepository ChildRepository)
        {
            _ChildRepository = ChildRepository;
        }

        public async Task<ValidationResult> Handle(UpdateChildCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;          

            var existingChild = await _ChildRepository.GetChildByCpf(message.Cpf);

            existingChild.FirstName = message.FirstName;
            existingChild.LastName = message.LastName;
            existingChild.Email = new Email(message.Email);
            existingChild.Cpf = new Cpf(message.Cpf);
            existingChild.Phones = message.Phones;
            existingChild.Address = message.Address;
            existingChild.BirthDate = DateTime.Parse(message.BirthDate);
            existingChild.Gender = (Gender)Enum.Parse(typeof(Gender), message.Gender, true);
            existingChild.FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), message.Function, true);
            existingChild.PhotoUrl = message.PhotoUrl;
            existingChild.Excluded = message.Excluded;
           
            await _ChildRepository.UpdateChild(existingChild);

            existingChild.AddEvent(new UpdatedChildEvent 
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
            
            return await PersistData(_ChildRepository.UnitOfWork);
        }
    }
}
