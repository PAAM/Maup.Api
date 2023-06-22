using Maup.Core.Entities;
using Maup.Core.Repositories;

namespace Maup.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Property> PropertyRepository { get; }

        IRepository<PropertyImage> PropertyImageRepository { get; }

        IOwnerRepository OwnerRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
