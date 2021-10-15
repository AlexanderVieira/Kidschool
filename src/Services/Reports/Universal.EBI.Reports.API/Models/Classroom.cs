using System;
using System.Collections.Generic;

namespace Universal.EBI.Reports.API.Models
{
    public class Classroom
    {
        public Guid Id { get; set; }
        public string Region { get; set; }
        public string Church { get; set; }
        public string ClassroomType { get; set; }
        public string Lunch { get; set; }
        public DateTime MeetingTime { get; set; }
        public Educator Educator { get; set; }
        public ICollection<Child> Childs { get; set; }
        public bool Actived { get; set; }
    }
}
