using System.Collections.Generic;

namespace Universal.EBI.Classrooms.API.Application.DTOs
{
    public class ResponsibleDto : PersonDto
    {
        public string KinshipType { get; set; }
        public List<PhoneDto> Phones { get; set; }
        public AddressDto Address { get; set; } 

    }
}