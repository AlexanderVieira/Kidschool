using System;
using Universal.EBI.Classrooms.API.Models;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Classrooms.API.Application.Events
{
    public class RegisteredClassroomEvent : Event
    {
        public Guid Id { get; set; }
        public string Region { get; set; }
        public string Church { get; set; }
        public string ClassroomType { get; set; }
        public string MeetingTime { get; set; }
        public Educator Educator { get; set; }
        public Child[] Childs { get; set; }
        public bool Actived { get; set; }

    }
}
