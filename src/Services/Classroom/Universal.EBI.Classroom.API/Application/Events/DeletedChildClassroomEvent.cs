using System;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Classrooms.API.Application.Events
{
    public class DeletedChildClassroomEvent : Event
    {
        public Guid ClassroomId { get; set; }
        public Guid ChildId { get; set; }
    }
}
