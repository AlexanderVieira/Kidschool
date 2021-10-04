using Universal.EBI.Core.DomainObjects.Exceptions;
using System.Text.RegularExpressions;

namespace Universal.EBI.Core.DomainObjects
{
    public class Email
    {
        public const int ADDRESS_MAX_LENGTH = 254;
        public const int ADDRESS_MIN_LENGTH = 5;
        public string Address { get; private set; }

        protected Email()
        {
        }
        public Email(string address)
        {
            if (!EmailValid(address)) throw new DomainException("Email inválido.");
            Address = address;
        }

        public static bool EmailValid(string email)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
        }
    }
}
