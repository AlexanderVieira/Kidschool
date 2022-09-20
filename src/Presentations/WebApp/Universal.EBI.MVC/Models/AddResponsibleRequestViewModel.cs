using System;
using System.Collections.Generic;

namespace Universal.EBI.MVC.Models
{
    public class AddResponsibleRequestViewModel
    {
        public Guid ChildId { get; set; }
        public ResponsibleRequestViewModel ResponsibleViewModel { get; set; }

        public AddResponsibleRequestViewModel()
        {
            ResponsibleViewModel = new ResponsibleRequestViewModel
            {
                Address = new AddressRequestViewModel(),
                Phones = new List<PhoneRequestViewModel>
                {
                    new PhoneRequestViewModel()
                }
            };
        }
    }
}
