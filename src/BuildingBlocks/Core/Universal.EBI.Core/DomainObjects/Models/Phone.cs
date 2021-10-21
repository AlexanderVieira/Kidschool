using System;
using System.Text.Json.Serialization;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Core.DomainObjects.Models
{
    public class Phone
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public PhoneType PhoneType { get; set; }
        public Guid? ChildId { get; set; }
        public Guid? ResponsibleId { get; set; }
        public Guid? EducatorId { get; set; }

        [JsonIgnore]
        public virtual Child Child { get; set; }

        [JsonIgnore]
        public virtual Responsible Responsible { get; set; }

        [JsonIgnore]
        public virtual Educator Educator { get; set; }
    }
}