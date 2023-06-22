using Maup.Core.Entities;
using Maup.Core.Interfaces;

namespace Maup.Core.Repositories
{
    public interface IOwnerRepository : IRepository<Owner>
    {
        Task<Owner> GetLoginByCredentials(Owner owner);

    }
}