using Universal.EBI.Core.Messages;

namespace Universal.EBI.Responsibles.API.Application.DTOs
{
    public class ChildDto : Command
    {             
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public AddressDto Address { get; set; }
        public PhoneDto[] Phones { get; set; }        
        public string BirthDate { get; set; }
        public string PhotoUrl { get; set; }
        public string Gender { get; set; }
        public string AgeGroup { get; set; }
        public bool Excluded { get; set; }        
        
    }
}
