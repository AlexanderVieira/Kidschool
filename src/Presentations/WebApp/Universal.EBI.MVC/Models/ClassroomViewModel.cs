using System;
using System.Collections.Generic;

namespace Universal.EBI.MVC.Models
{
    public class ClassroomViewModel
    {
        public Guid Id { get; set; }
        public string Region { get; set; }
        public string Church { get; set; }
        public string Lunch { get; set; }
        public string ClassroomType { get; set; }      
        public string MeetingTime { get; set; }        
        public EducatorClassroomViewModel Educator { get; set; }
        public List<ChildViewModel> Childs { get; set; }
        public bool Actived { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDate { get; set; }

        public ClassroomViewModel()
        {
            Childs = new List<ChildViewModel>();
        }        
        
    }
}
