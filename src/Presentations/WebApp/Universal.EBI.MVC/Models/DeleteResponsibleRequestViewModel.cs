using System;

namespace Universal.EBI.MVC.Models
{
    public class DeleteResponsibleRequestViewModel
    {
        public Guid ChildId { get; set; }
        public ResponsibleRequestViewModel ResponsibleViewModel { get; set; }

        public DeleteResponsibleRequestViewModel()
        {
        }
    }
}
