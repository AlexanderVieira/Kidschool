using System;

namespace Universal.EBI.Childs.API.Application.DTOs
{
    public class PhoneRequestDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string PhoneType { get; set; }        

        public PhoneRequestDto()
        {
        }

    }
    
}
