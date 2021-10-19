using System;
using Universal.EBI.Classrooms.API.Models.Enums;
using Universal.EBI.Core.DomainObjects;

namespace Universal.EBI.Reports.API.Models
{
    public class Person : Entity
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public bool Excluded { get; set; }
        public Email Email { get; set; }
        public string Cpf { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime BirthDate { get; set; }
        
        private DateTime? _createdAt;
        public DateTime? CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = (value == null ? DateTime.UtcNow : value); }
        }
        public DateTime? UpdatedAt { get; set; }
        public GenderType GenderType { get; set; }        
    }
}
