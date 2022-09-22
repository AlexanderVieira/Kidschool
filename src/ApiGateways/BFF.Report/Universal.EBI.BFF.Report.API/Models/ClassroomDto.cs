using System;

namespace Universal.EBI.BFF.Report.API.Models
{
    public class ClassroomDto
    {
        public Guid Id { get; set; }
        public string Region { get; set; }
        public string Church { get; set; }
        public string Lunch { get; set; }
        public string ClassroomType { get; set; }
        public string MeetingTime { get; set; }
        public EducatorDto Educator { get; set; }
        //public ChildResponseDto[] Childs { get; set; }
        //public bool Actived { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public string LastModifiedBy { get; set; }
        //public DateTime? LastModifiedDate { get; set; }
    }
}
