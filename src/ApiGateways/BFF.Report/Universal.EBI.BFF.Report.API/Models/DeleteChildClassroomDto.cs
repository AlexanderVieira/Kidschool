using System;

namespace Universal.EBI.BFF.Report.API.Models
{
    public class DeleteChildClassroomDto
    {
        public Guid ClassroomId { get; set; }
        public Guid ChildId { get; set; }
    }
}
