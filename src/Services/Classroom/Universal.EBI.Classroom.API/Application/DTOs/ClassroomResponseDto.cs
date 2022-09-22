using System;

namespace Universal.EBI.Classrooms.API.Application.DTOs
{
    public class ClassroomResponseDto 
    {
        public Guid Id { get; set; }
        public string Region { get; set; }
        public string Church { get; set; }
        public string Lunch { get; set; }
        public string ClassroomType { get; set; }      
        public string MeetingTime { get; set; }        
        public EducatorDto Educator { get; set; }        

        public ClassroomResponseDto()
        {            
        }              
        
    }
}
