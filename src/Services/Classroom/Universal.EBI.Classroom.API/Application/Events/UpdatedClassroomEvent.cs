﻿using System;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.Messages;

namespace Universal.EBI.Classrooms.API.Application.Events
{
    public class UpdatedClassroomEvent : Event
    {
        public Guid Id { get; set; }
        public string Region { get; set; }
        public string Church { get; set; }
        public string Lunch { get; set; }
        public string ClassroomType { get; set; }
        public string MeetingTime { get; set; }
        public Educator Educator { get; set; }
        public Child[] Childs { get; set; }
        public bool Actived { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDate { get; set; }

    }
}