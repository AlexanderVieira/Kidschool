using FluentValidation.Results;
using System.Threading.Tasks;
using Universal.EBI.Core.Data.Interfaces;

namespace Universal.EBI.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected async Task<ValidationResult> PersistData(IUnitOfWork unitOfWork)
        {
            if (!await unitOfWork.Commit()) AddError("Houve um erro ao persistir os dados.");
            return ValidationResult;
        }

        protected async Task<ValidationResult> PersistData(IUnitOfWork unitOfWork, bool success)
        {
            if (!await unitOfWork.Commit(success)) AddError("Houve um erro ao persistir os dados.");
            return ValidationResult;
        }
    }
}
