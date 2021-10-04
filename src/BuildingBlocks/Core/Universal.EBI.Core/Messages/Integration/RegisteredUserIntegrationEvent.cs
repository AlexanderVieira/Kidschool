using System;

namespace Universal.EBI.Core.Messages.Integration
{
    public class RegisteredUserIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; private set; }        
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public RegisteredUserIntegrationEvent(Guid id, string firstName, string lastName, string email, string cpf)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Cpf = cpf;
        }
    }
}
