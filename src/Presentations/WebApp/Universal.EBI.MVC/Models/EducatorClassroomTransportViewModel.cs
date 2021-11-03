using System;

namespace Universal.EBI.MVC.Models
{
    public class EducatorClassroomTransportViewModel
    {
        public Guid ClassroomId { get; set; }
        public string Region { get; set; }
        public string Church { get; set; }
        public string Lunch { get; set; }
        public string ClassroomType { get; set; }
        public string MeetingTime { get; set; }
        public bool Actived { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDate { get; set; }
        
        //Educador
        public Guid EducatorId { get; set; }
        public string EducatorFirstName { get; set; }
        public string EducatorLastName { get; set; }
        public string EducatorFunctionType { get; set; }
    }
}
