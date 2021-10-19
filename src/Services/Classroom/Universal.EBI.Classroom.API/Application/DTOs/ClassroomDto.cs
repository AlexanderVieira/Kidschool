using System;
using System.Collections.Generic;
using System.Linq;
using Universal.EBI.Classrooms.API.Models;
using Universal.EBI.Classrooms.API.Models.Enums;

namespace Universal.EBI.Classrooms.API.Application.DTOs
{
    public class ClassroomDto
    {
        public Guid Id { get; set; }
        public string Region { get; set; }
        public string Church { get; set; }
        public string ClassroomType { get; set; }      
        public string MeetingTime { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public EducatorDto Educator { get; set; }
        public Dictionary<string, ChildDto> Childs { get; set; }
        public bool Actived { get; set; }

        public ClassroomDto()
        {
            Childs = new Dictionary<string, ChildDto>();
        }

        public Classroom ToConvertClassroom(ClassroomDto classroomDto)
        {
            var classroom = new Classroom
            {
                Id = classroomDto.Id,
                Educator = new Educator(),
                Church = classroomDto.Church,
                Region = classroomDto.Region,
                ClassroomType = (ClassroomType)Enum.Parse(typeof(ClassroomType), classroomDto.ClassroomType, true),                
                Actived = classroomDto.Actived,
                MeetingTime = classroomDto.MeetingTime,
                Childs = new List<Child>() 
            };

            var educator = classroomDto.Educator.ToConvertEducator(classroomDto.Educator);
            classroom.Educator = educator;

            foreach (var item in classroomDto.Childs)
            {
                if (classroomDto.Childs.TryGetValue(item.Key.ToString(), out ChildDto childDto))
                {
                    var child = childDto.ToConvertChild(childDto);
                    classroom.Childs.Add(child);                    
                }
            }
            classroom.Childs.ToArray();
            return classroom;

        }
        
    }
}
