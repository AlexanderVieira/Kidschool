using System.Collections.Generic;

namespace Universal.EBI.BFF.Report.API.Models
{
    public class ResponsibleDto : PersonDto
    {
        public int KinshipType { get; set; }
        public List<PhoneDto> Phones { get; set; }
    }
}