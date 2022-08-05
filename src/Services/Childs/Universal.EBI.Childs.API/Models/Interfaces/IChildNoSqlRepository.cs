using System;
using System.Threading.Tasks;

namespace Universal.EBI.Childs.API.Models.Interfaces
{
    public interface IChildNoSqlRepository
    {
        Task<Child> CreateChild(Child child);
        Task<bool> UpdateChild(Child child);
        Task<bool> DeleteChild(Guid id);        
        Task<IChildContext> GetContext();        
               

    }
}
