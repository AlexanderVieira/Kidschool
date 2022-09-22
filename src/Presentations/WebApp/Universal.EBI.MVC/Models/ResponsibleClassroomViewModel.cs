using System;
using System.Collections.Generic;

namespace Universal.EBI.MVC.Models
{
    public class ResponsibleClassroomViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }        
        public string Cpf { get; set; }        
        public DateTime BirthDate { get; set; }
        public string GenderType { get; set; }
        public string KinshipType { get; set; }
        public List<PhoneViewModel> Phones { get; set; }        

    }
}