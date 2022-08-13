using System;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Childs.API.Models
{
    public class ChildDesignedQuery
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderType GenderType { get; set; }
    }
}
