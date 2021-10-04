using System;
using System.Text.Json.Serialization;
using Universal.EBI.Core.DomainObjects;

namespace Universal.EBI.Educators.API.Models
{
    public class Phone : Entity
    {
        public string Number { get; set; }
        public PhoneType PhoneType { get; set; }
        public Guid EducatorId { get; set; }
        
        [JsonIgnore]
        public virtual Educator Educator { get; set; }

    }
    
}
