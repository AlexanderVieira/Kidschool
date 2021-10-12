using System.Collections.Generic;

namespace Universal.EBI.BFF.Report.API.Models
{
    public class ChildDto : PersonDto
    {
        public string StartTimeMeeting { get; set; }
        public string EndTimeMeeting { get; set; }
        public int AgeGroupType { get; set; }
        public List<PhoneDto> Phones { get; set; }
        public List<ResponsibleDto> Responsibles { get; set; }
    }
}