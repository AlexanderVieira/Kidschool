using System;
using System.Collections.Generic;
using Universal.EBI.Childs.API.Models;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Childs.API.Application.DTOs
{
    public class ChildDto : PersonDto
    {
        public string AgeGroupType { get; set; }
        public List<PhoneRequestDto> Phones { get; set; }
        public List<ResponsibleDto> Responsibles { get; set; }

    }
}
