using System;
using System.Collections.Generic;

namespace Universal.EBI.Childs.API.Application.DTOs
{
    public class ChildResponseDto
    {
        public Guid Id { get; set; }        
        public string FullName { get; set; } 
        public string PhotoUrl { get; set; }
        public string BirthDate { get; set; }
        public string GenderType { get; set; } 
        public ICollection<ResponsibleDto> Responsibles { get; set; }

    }
}
