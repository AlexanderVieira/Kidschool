using System;

namespace Universal.EBI.MVC.Models
{
    public class PhoneRequestViewModel
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string PhoneType { get; set; }        

        public PhoneRequestViewModel()
        {
        }

    }
    
}
