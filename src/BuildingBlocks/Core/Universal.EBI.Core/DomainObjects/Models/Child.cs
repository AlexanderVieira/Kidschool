using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Universal.EBI.Core.DomainObjects.Interfaces;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Core.DomainObjects.Models
{
    public class Child : Person, IAggregateRoot
    {
        public Guid Id { get; set; }
        public string HoraryOfEntry { get; set; }
        public string HoraryOfExit { get; set; }
        public AgeGroupType AgeGroupType { get; set; }         
        public virtual Address Address { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
        public virtual ICollection<Responsible> Responsibles { get; set; }
        public Guid? ClassroomId { get; set; }

        [JsonIgnore]
        public virtual Classroom Classroom { get; set; }
    }
}