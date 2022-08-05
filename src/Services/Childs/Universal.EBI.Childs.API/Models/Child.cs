using System;
using System.Collections.Generic;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Interfaces;
using Universal.EBI.Core.DomainObjects.Models.Enums;

namespace Universal.EBI.Childs.API.Models
{
    public class Child : Entity, IAggregateRoot
    {     
        public string FirstName { get; set; }                
        public string LastName { get; set; }
        public string FullName { get; set; }
        public bool Excluded { get; set; }
        public Email Email { get; set; }
        public Cpf Cpf { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public AgeGroupType AgeGroupType { get; set; }
        public GenderType GenderType { get; set; }        
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Address Address { get; set; }
        public IList<Phone> Phones { get; set; }
        public IList<Responsible> Responsibles { get; set; }

        public Child()
        {            
            Responsibles = new List<Responsible>();
            Phones = new List<Phone>();            
        }
        
        //private List<Event> _notifications;
       
        //public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();

        //public void AddEvent(Event myEvent)
        //{
        //    _notifications ??= new List<Event>();
        //    _notifications.Add(myEvent);
        //}

        //public void RemoveEvent(Event myEvent)
        //{
        //    _notifications?.Remove(myEvent);
        //}

        //public void ClearEvent()
        //{
        //    _notifications?.Clear();
        //}

    }
}
