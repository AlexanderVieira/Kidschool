using System;
using System.Collections.Generic;

namespace Universal.EBI.Reports.API.Models
{
    public class Child : Person
    {
        public DateTime? StartTimeMeeting { get; set; }
        public DateTime? EndTimeMeeting { get; set; }
        public int AgeGroupType { get; set; }
        public ICollection<Phone> Phones { get; set; }
        public ICollection<Responsible> Responsibles { get; set; }
    }
}