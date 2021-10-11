using System;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Classrooms.API.Application.Events
{
    public class DeletedClassroomEvent : Event
    {
        public Guid Id { get; set; }        
       
    }
}
