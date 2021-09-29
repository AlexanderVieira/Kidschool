using FluentValidation.Results;

namespace Universal.EBI.Core.Messages
{
    public class ResponseMessage
    {
        public ValidationResult ValidationResult { get; set; }

        public ResponseMessage(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}
