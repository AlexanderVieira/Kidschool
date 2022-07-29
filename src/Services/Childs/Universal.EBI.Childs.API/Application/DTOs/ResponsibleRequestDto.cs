using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Universal.EBI.Childs.API.Application.DTOs
{
    public class ResponsibleRequestDto
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
        public string KinshipType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public ICollection<PhoneRequestDto> Phones { get; set; }
        public ICollection<ChildRequestDto> Children { get; set; }
    }
}
