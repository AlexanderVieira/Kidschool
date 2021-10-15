using System;

namespace Universal.EBI.Reports.API.Models
{
    public class Phone
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public int PhoneType { get; set; }
        public Guid? ForeingKeyId { get; set; }
    }
}