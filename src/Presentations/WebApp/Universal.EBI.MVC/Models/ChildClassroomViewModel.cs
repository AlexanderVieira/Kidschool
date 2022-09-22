using System;
using System.Collections.Generic;

namespace Universal.EBI.MVC.Models
{
    public class ChildClassroomViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }        
        public DateTime BirthDate { get; set; }
        public string AgeGroupType { get; set; }
        public string GenderType { get; set; }
        //public string HoraryOfEntry { get; set; }
        //public string HoraryOfExit { get; set; }
        public List<ResponsibleClassroomViewModel> Responsibles { get; set; }
        
    }
}
