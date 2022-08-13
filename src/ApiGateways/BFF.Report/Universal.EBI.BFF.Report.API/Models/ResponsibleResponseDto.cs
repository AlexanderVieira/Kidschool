using System;
using System.Collections.Generic;

namespace Universal.EBI.BFF.Report.API.Models
{
    public class ResponsibleResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public bool Excluded { get; set; }
        public string AddressEmail { get; set; }
        public string NumberCpf { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public string GenderType { get; set; }        
        public string KinshipType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public AddressResponseDto Address { get; set; }
        public IList<PhoneResponseDto> Phones { get; set; }        

        public ResponsibleResponseDto()
        {
        }
    }
}
