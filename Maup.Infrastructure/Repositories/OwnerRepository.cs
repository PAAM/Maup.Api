using Maup.Core.Entities;
using Maup.Core.Interfaces;
using Maup.Core.Repositories;
using Maup.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Infrastructure.Repositories
{
    public class OwnerRepository : Repository<Owner>, IOwnerRepository
    {
        public OwnerRepository(MUPContext context) : base(context) { }

        public async Task<Owner> GetLoginByCredentials(Owner owner)
        {
            return await _entities.FirstOrDefaultAsync(e => e.User == owner.User && e.Password == owner.Password);
        }
    }
}
