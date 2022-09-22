using System;
using System.Collections.Generic;
using System.Linq;
using Universal.EBI.Classrooms.API.Models;
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
        public ChildDto[] Childs { get; set; }
        public bool Actived { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public ClassroomDto()
        {            
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
                CreatedDate = classroomDto.CreatedDate.Value,
                CreatedBy = classroomDto.CreatedBy,
                LastModifiedDate = classroomDto.LastModifiedDate,
                LastModifiedBy = classroomDto.LastModifiedBy,
                Children = new List<Child> () 
            };

            var educator = classroomDto.Educator.ToConvertEducator(classroomDto.Educator);
            classroom.Educator = educator;
            var dictionary = classroomDto.Childs != null ? classroomDto.Childs.ToDictionary(c => c.Id.ToString()) : new Dictionary<string, ChildDto>();
            foreach (var item in dictionary)
            {
                if (dictionary.TryGetValue(item.Key.ToString(), out ChildDto childDto))
                {
                    var child = childDto.ToConvertChild(childDto);
                    classroom.Children.Add(child);
                }
            }
            classroom.Children.ToArray();
            return classroom;            
        }

        public ClassroomDto ToConvertClassroomDto(Classroom classroom)
        {
            var classroomDto = new ClassroomDto
            {
                Id = classroom.Id,
                Region = classroom.Region,
                Church = classroom.Church,
                MeetingTime = classroom.MeetingTime,
                ClassroomType = classroom.ClassroomType.ToString(),
                Lunch = classroom.Lunch,
                Actived = classroom.Actived,
                CreatedBy = classroom.CreatedBy,
                CreatedDate = classroom.CreatedDate,
                LastModifiedBy = classroom.LastModifiedBy,
                LastModifiedDate = classroom.LastModifiedDate,
                Educator = new EducatorDto().ToConvertEducatorDto(classroom.Educator),
                Childs = new List<ChildDto>().ToArray()
            };

            var dictionary = classroom.Children != null ? classroom.Children.ToDictionary(c => c.Id.ToString()) : new Dictionary<string, Child>();
            foreach (var item in dictionary)
            {
                if (dictionary.TryGetValue(item.Key.ToString(), out Child child))
                {
                    var childDto = new ChildDto();
                    childDto = childDto.ToConvertChildDto(child);
                    classroomDto.Childs[0] = childDto;
                }
            }
            
            return classroomDto;
        }

    }
}
