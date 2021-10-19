using System;
using System.Collections.Generic;
using Universal.EBI.Classrooms.API.Models.Enums;

namespace Universal.EBI.Classrooms.API.Models
{
    public class Child : Person
    {
        public string HoraryOfEntry { get; set; }
        public string HoraryOfExit { get; set; }
        public AgeGroupType AgeGroupType { get; set; }              
        public ICollection<Phone> Phones { get; set; }        
        public ICollection<Responsible> Responsibles { get; set; }

        public Child()
        {
            Phones = new HashSet<Phone>();
            Responsibles = new HashSet<Responsible>();
        }
                
    }
}