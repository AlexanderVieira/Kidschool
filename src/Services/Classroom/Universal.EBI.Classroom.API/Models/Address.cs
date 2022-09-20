using Universal.EBI.Core.DomainObjects;

namespace Universal.EBI.Classrooms.API.Models
{
    public class Address : Entity
    {        
        public string PublicPlace { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }                
                
        public Address() 
        {            
        }

        //public Address(string publicPlace, string number, string complement, string district, 
        //               string zipCode, string city, string state, Guid? foreingKeyId)
        //{            
        //    PublicPlace = publicPlace;
        //    Number = number;
        //    Complement = complement;
        //    District = district;
        //    ZipCode = zipCode;
        //    City = city;
        //    State = state;
        //    ForeingKeyId = foreingKeyId;
        //}
    }
}