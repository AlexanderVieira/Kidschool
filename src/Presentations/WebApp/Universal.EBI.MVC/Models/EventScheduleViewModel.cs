using System;

namespace Universal.EBI.MVC.Models
{
    public class EventScheduleViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public bool AllDay { get; set; }
    }
}
