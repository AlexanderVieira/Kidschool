using Universal.EBI.Core.DomainObjects;

namespace Universal.EBI.Core.Utils
{
    public static class ValidationUtils
    {
        public static bool HasValidEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                return Email.EmailValid(email);
            }
            else
            {
                return true;
            }
        }

        public static bool HasValidCpf(string strCpf)
        {
            if (!string.IsNullOrWhiteSpace(strCpf))
            {
                return Cpf.CpfValid(strCpf);
            }
            else
            {
                return true;
            }
        }

        public static bool HasValidBirthDate(string date)
        {
            if (!string.IsNullOrWhiteSpace(date))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static Email ValidateRequestEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                return new Email(email);
            }
            else
            {
                return null;
            }
        }

        public static Cpf ValidateRequestCpf(string cpf)
        {
            if (!string.IsNullOrWhiteSpace(cpf))
            {
                return new Cpf(cpf);
            }
            else
            {
                return null;
            }
        }
        
    }
}
