using System;

namespace Universal.EBI.Childs.API.Application.DTOs
{
    public class InactivateChildRequestDto
    {
        public Guid Id { get; set; }        
        public string FullName { get; set; }
        public bool Excluded { get; set; }        
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }        

        public InactivateChildRequestDto()
        {
        }
    }
}
