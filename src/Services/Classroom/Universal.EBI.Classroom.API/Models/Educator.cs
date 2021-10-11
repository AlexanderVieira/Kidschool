using System.Collections.Generic;
using Universal.EBI.Classrooms.API.Models.Enums;

namespace Universal.EBI.Classrooms.API.Models
{
    public class Educator : Person
    {   
        public FunctionType? FunctionType { get; set; }        
        public ICollection<Phone> Phones { get; set; }        
        
        public Educator()
        {
            Phones = new HashSet<Phone>();
        }        
        
    }   
    
}
