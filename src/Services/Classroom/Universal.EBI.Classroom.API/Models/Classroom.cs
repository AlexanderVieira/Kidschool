using System.Collections.Generic;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Interfaces;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Classrooms.API.Models
{
    public class Classroom : Entity, IAggregateRoot
    {        
        public string Region { get; set; }
        public string Church { get; set; }
        public ClassroomType ClassroomType { get; set; }
        public string Lunch { get; set; }
        public string MeetingTime { get; set; }        
        public bool Actived { get; set; }
        public virtual Educator Educator { get; set; }
        public virtual List<Child> Children { get; set; }        

        public Classroom()
        {
            Children = new List<Child>();
        }
    }
}
