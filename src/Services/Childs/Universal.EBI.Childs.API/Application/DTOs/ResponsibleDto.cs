using System;
using System.Collections.Generic;
using Universal.EBI.Childs.API.Models;

namespace Universal.EBI.Childs.API.Application.DTOs
{
    public class ResponsibleDto : PersonDto
    {
        public string KinshipType { get; set; }
        public List<PhoneDto> Phones { get; set; }

    }
}