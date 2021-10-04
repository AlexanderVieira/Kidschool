using FluentValidation.Results;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Childs.API.Application.Events;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.Commands
{
    public class RegisterChildCommandHandler : CommandHandler, IRequestHandler<RegisterChildCommand, ValidationResult>
    {
        private readonly IChildRepository _ChildRepository;

        public RegisterChildCommandHandler(IChildRepository ChildRepository)
        {
            _ChildRepository = ChildRepository;
        }

        public async Task<ValidationResult> Handle(RegisterChildCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;
                        
            var child = new Child
            {
                Id = message.Id,
                FirstName = message.FirstName,
                LastName = message.LastName,
                Email = new Email(message.Email),
                Cpf = new Cpf(message.Cpf),
                Phones = message.Phones,
                Address = message.Address,
                BirthDate = DateTime.Parse(message.BirthDate),
                Gender = (Gender)Enum.Parse(typeof(Gender), message.Gender, true),
                FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), message.Function, true),
                PhotoUrl = message.PhotoUrl,
                Excluded = message.Excluded
            };
                        
            var phonesCount = child.Phones.Count;
            for (int i = 0; i < phonesCount; i++)
            {
                child.Phones.ToList()[i].ChildId = child.Id;                               
            }

            child.Address = new Address(child.Address.PublicPlace,
                                           child.Address.Number,
                                           child.Address.Complement,
                                           child.Address.District,
                                           child.Address.ZipCode,
                                           child.Address.City,
                                           child.Address.State,
                                           child.Id);

            var existingCustomer = await _ChildRepository.GetChildByCpf(child.Cpf.Number);

            if (existingCustomer != null)
            {
                AddError("Este CPF já está em uso.");
                return ValidationResult;
            }

            await _ChildRepository.CreateChild(child);

            child.AddEvent(new RegisteredChildEvent 
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
