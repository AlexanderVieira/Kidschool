using System;

namespace Universal.EBI.BFF.Report.API.Models
{
    public class AddResponsibleRequestDto
    {
        public Guid ChildId { get; set; }
        public ResponsibleRequestDto ResponsibleDto { get; set; }

        public AddResponsibleRequestDto()
        {
        }
    }
}
