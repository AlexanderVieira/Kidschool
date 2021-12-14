using System;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Classrooms.API.Models
{
    public class Educator
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public FunctionType FunctionType { get; set; }
        public string PhotoUrl { get; set; }        
        public Guid ClassroomId { get; set; }

    }   
    
}
