using System.Collections.Generic;

namespace Universal.EBI.MVC.Models
{
    public class ResponsibleViewModel : PersonViewModel
    {
        public string KinshipType { get; set; }
        public List<PhoneViewModel> Phones { get; set; }
        public AddressViewModel Address { get; set; } 

    }
}