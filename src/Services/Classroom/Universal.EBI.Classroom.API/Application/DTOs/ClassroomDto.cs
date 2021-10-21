using System;
using System.Collections.Generic;
using System.Linq;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Classrooms.API.Application.DTOs
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
        public Dictionary<string, ChildDto> Childs { get; set; }
        public bool Actived { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDate { get; set; }

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
                Lunch = classroomDto.Lunch,
                ClassroomType = (ClassroomType)Enum.Parse(typeof(ClassroomType), classroomDto.ClassroomType, true),                
                Actived = classroomDto.Actived,
                MeetingTime = classroomDto.MeetingTime,
                CreatedDate = DateTime.Parse(classroomDto.CreatedDate),
                CreatedBy = classroomDto.CreatedBy,
                Children = new List<Child>() 
            };

            var educator = classroomDto.Educator.ToConvertEducator(classroomDto.Educator);
            classroom.Educator = educator;

            foreach (var item in classroomDto.Childs)
            {
                if (classroomDto.Childs.TryGetValue(item.Key.ToString(), out ChildDto childDto))
                {
                    var child = childDto.ToConvertChild(childDto);
                    classroom.Children.Add(child);                    
                }
            }
            classroom.Children.ToArray();
            return classroom;

        }
        
    }
}
