using System.ComponentModel.DataAnnotations;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.MVC.Properties;

namespace Universal.EBI.MVC.Extensions.CpfAnnotations
{
    public class CpfAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return Cpf.CpfValid(value.ToString()) ? ValidationResult.Success : new ValidationResult(Resources.MSG_ERROR_CPF_INVALIDO);
        }
    }
}
