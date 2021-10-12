using System.Collections.Generic;

namespace Universal.EBI.Classrooms.API.Application.DTOs
{
    public class ResponsibleDto : PersonDto
    {
        public int KinshipType { get; set; }
        public List<PhoneDto> Phones { get; set; }        

    }
}