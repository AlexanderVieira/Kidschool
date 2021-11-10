using System.Collections.Generic;

namespace Universal.EBI.BFF.Report.API.Models
{
    public class EducatorDto : PersonDto
    {
        public string FunctionType { get; set; }
        public List<PhoneDto> Phones { get; set; }
    }
}