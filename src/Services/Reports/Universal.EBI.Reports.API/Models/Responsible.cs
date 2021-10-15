using System.Collections.Generic;

namespace Universal.EBI.Reports.API.Models
{
    public class Responsible : Person
    {
        public string KinshipType { get; set; }
        public ICollection<Phone> Phones { get; set; }
    }
}