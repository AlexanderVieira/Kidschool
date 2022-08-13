using System;

namespace Universal.EBI.BFF.Report.API.Models
{
    public class PhoneResponseDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string PhoneType { get; set; }

        public PhoneResponseDto()
        {
        }

    }
    
}
