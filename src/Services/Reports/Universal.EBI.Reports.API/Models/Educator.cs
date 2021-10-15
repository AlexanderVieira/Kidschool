using System.Collections.Generic;

namespace Universal.EBI.Reports.API.Models
{
    public class Educator : Person
    {
        public int FunctionType { get; set; }
        public ICollection<Phone> Phones { get; set; }
    }
}