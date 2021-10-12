using System;

namespace Universal.EBI.BFF.Report.API.Models
{
    public class ClassroomDto
    {
        public Guid Id { get; set; }
        public string Region { get; set; }
        public string Church { get; set; }
        public int ClassroomType { get; set; }
        public string MeetingTime { get; set; }
        public EducatorDto Educator { get; set; }
        public ChildDto[] Childs { get; set; }
        public bool Actived { get; set; }        
    }
}
