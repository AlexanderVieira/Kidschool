using System;

namespace Universal.EBI.Childs.API.Application.DTOs
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
