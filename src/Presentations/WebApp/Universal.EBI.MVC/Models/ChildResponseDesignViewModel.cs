using System;

namespace Universal.EBI.MVC.Models
{
    public class ChildResponseDesignViewModel
    { 
        public Guid Id { get; set; }        
        public string FullName { get; set; }        
        public string PhotoUrl { get; set; }
        public string BirthDate { get; set; }
        public string GenderType { get; set; }              
        
    }
}
