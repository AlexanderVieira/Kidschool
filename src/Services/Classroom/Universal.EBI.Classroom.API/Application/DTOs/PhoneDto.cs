using System;

namespace Universal.EBI.Classrooms.API.Application.DTOs
{
    public class PhoneDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string PhoneType { get; set; }
        public Guid? ForeingKeyId { get; set; }

    }
    
}
