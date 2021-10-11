using System.Collections.Generic;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Interfaces;
using Universal.EBI.Educators.API.Models.Enums;

namespace Universal.EBI.Educators.API.Models
{
    public class Educator : Person, IAggregateRoot
    {               
        public bool Excluded { get; set; }
        public FunctionType? FunctionType { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }        

        // EF Relation
        public Educator()
        {
            Phones = new HashSet<Phone>();
        }        

        public void ChangeEmail(string email)
        {
            Email = new Email(email);
        }

        public void AssignAddress(Address adddress)
        {
            Address = adddress;
        }
    }  
    
}
