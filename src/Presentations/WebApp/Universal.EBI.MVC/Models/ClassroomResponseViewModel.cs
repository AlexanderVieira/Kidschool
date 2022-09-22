using System;
using System.Collections.Generic;

namespace Universal.EBI.MVC.Models
{
    public class ClassroomResponseViewModel
    {
        public Guid Id { get; set; }
        public string Region { get; set; }
        public string Church { get; set; }
        public string Lunch { get; set; }
        public string ClassroomType { get; set; }      
        public string MeetingTime { get; set; }        
        public EducatorClassroomViewModel Educator { get; set; }
        //public List<ChildClassroomViewModel> Children { get; set; }
        //public bool Actived { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public string LastModifiedBy { get; set; }
        //public DateTime? LastModifiedDate { get; set; }

        //public ClassroomResponseViewModel()
        //{
        //    Children = new List<ChildClassroomViewModel>();
        //}        
        
    }
}
