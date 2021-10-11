using System.Collections.Generic;
using Universal.EBI.Classrooms.API.Models.Enums;

namespace Universal.EBI.Classrooms.API.Models
{
    public class Responsible : Person
    {   
        public KinshipType KinshipType { get; set; }        
        public ICollection<Phone> Phones { get; set; }
              
        
        public Responsible()
        {
            //Id = Guid.NewGuid();            
            Phones = new HashSet<Phone>();            
        }

    }     

}
