using System;

namespace Universal.EBI.Classrooms.API.Application.DTOs
{
    public class DeleteChildClassroomDto
    {
        public Guid ClassroomId { get; set; }
        public Guid ChildId { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDate { get; set; }
    }
}
