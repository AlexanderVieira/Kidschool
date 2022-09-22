using System;

namespace Universal.EBI.BFF.Report.API.Models
{
    public class EducatorDto 
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FunctionType { get; set; }
        public string PhotoUrl { get; set; }
    }
}