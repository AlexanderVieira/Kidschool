using System.Collections.Generic;
using Universal.EBI.MVC.Models;

namespace Universal.EBI.MVC.Services
{
    public class StateService
    {
        public static List<StateViewModel> GetStates() 
        {
            var states = new List<StateViewModel>
            {
                new StateViewModel{ Initials = "SP", Name = "São Paulo"},
                new StateViewModel{ Initials= "RJ", Name = "Rio de Janeiro"},
                new StateViewModel{ Initials = "MG", Name = "Minas Gerais"}
            };

            return states;
        }
    }
}
