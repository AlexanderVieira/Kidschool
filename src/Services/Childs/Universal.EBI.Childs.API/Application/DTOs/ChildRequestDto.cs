using System;
using System.Collections.Generic;

namespace Universal.EBI.Childs.API.Application.DTOs
{
    public class ChildRequestDto
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
        public AddressDto Address { get; set; }
        public string AgeGroupType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public ICollection<PhoneRequestDto> Phones { get; set; }
        public ICollection<ResponsibleRequestDto> Responsibles { get; set; }

        public ChildRequestDto()
        {
        }
    }
}
