using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Universal.EBI.Core.Messages;
using Universal.EBI.Educators.API.Application.Events;
using Universal.EBI.Educators.API.Models.Interfaces;

namespace Universal.EBI.Educators.API.Application.Commands
{
    public class DeleteEducatorCommandHandler : CommandHandler, IRequestHandler<DeleteEducatorCommand, ValidationResult>
    {
        private readonly IEducatorRepository _educatorRepository;

        public DeleteEducatorCommandHandler(IEducatorRepository educatorRepository)
        {
            _educatorRepository = educatorRepository;
        }

        public async Task<ValidationResult> Handle(DeleteEducatorCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;
           
            var existingEducator = await _educatorRepository.GetEducatorById(message.Id);

            if (existingEducator == null)
            {
                AddError("Educador não encontrado.");
                return ValidationResult;
            }

            await _educatorRepository.DeleteEducator(existingEducator.Id);

            existingEducator.AddEvent(new UpdatedEducatorEvent 
            {
                AggregateId = message.Id,
                Id = message.Id                
            });            

            return await PersistData(_educatorRepository.UnitOfWork);
        }
    }
}
