using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.Messages;
using Universal.EBI.Educators.API.Application.Events;
using Universal.EBI.Educators.API.Models;
using Universal.EBI.Educators.API.Models.Interfaces;

namespace Universal.EBI.Educators.API.Application.Commands
{
    public class UpdateEducatorCommandHandler : CommandHandler, IRequestHandler<UpdateEducatorCommand, ValidationResult>
    {
        private readonly IEducatorRepository _educatorRepository;

        public UpdateEducatorCommandHandler(IEducatorRepository educatorRepository)
        {
            _educatorRepository = educatorRepository;
        }

        public async Task<ValidationResult> Handle(UpdateEducatorCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;          

            var existingEducator = await _educatorRepository.GetEducatorByCpf(message.Cpf);

            existingEducator.FirstName = message.FirstName;
            existingEducator.LastName = message.LastName;
            existingEducator.Email = new Email(message.Email);
            existingEducator.Cpf = new Cpf(message.Cpf);
            existingEducator.Phones = message.Phones;
            existingEducator.Address = message.Address;
            existingEducator.BirthDate = DateTime.Parse(message.BirthDate);
            existingEducator.Gender = (Gender)Enum.Parse(typeof(Gender), message.Gender, true);
            existingEducator.FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), message.Function, true);
            existingEducator.PhotoUrl = message.PhotoUrl;
            existingEducator.Excluded = message.Excluded;
           
            await _educatorRepository.UpdateEducator(existingEducator);

            existingEducator.AddEvent(new UpdatedEducatorEvent 
            { 
                AggregateId = message.Id,
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
            
            return await PersistData(_educatorRepository.UnitOfWork);
        }
    }
}
