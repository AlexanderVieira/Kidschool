using System.Collections.Generic;

namespace Universal.EBI.Childs.API.Application.DTOs
{
    public class ResponsibleDto : PersonDto
    {
        public string KinshipType { get; set; }
        public ICollection<PhoneRequestDto> Phones { get; set; }
        public ICollection<ChildDto> Children { get; set; }

        public ResponsibleDto()
        {
        }

    }
}