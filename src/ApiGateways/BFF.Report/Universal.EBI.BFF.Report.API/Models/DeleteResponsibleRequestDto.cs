using System;

namespace Universal.EBI.BFF.Report.API.Models
{
    public class DeleteResponsibleRequestDto
    {
        public Guid ChildId { get; set; }
        public ResponsibleRequestDto ResponsibleDto { get; set; }

        public DeleteResponsibleRequestDto()
        {
        }
    }
}
