using System;

namespace Universal.EBI.Reports.API.Models
{
    public class ChildResponsible
    {        
        public Guid ChildrenId { get; set; }
        public virtual Child Child { get; set; }
        public Guid ResponsiblesId { get; set; }
        public virtual Responsible Responsible { get; set; }

    }
}
