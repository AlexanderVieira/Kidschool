using System.Threading.Tasks;

namespace Universal.EBI.Core.Data.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();        
        
    }
}
