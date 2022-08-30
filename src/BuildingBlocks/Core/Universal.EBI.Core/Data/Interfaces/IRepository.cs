using System;
using Universal.EBI.Core.DomainObjects.Interfaces;

namespace Universal.EBI.Core.Data.Interfaces
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        
    }
}
