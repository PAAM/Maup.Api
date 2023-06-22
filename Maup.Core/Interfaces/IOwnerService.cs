using Maup.Core.Entities;
using Maup.Core.Filters;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Core.Interfaces
{
    public interface IOwnerService
    {
        Task CreateOwner(Owner owner, IFormFile formFile);
        IEnumerable<Owner> GetOwnerAsync();
        Task<Owner> GetOwnerById(int Id);
        Task<bool> UpdateOwner(Owner owner, IFormFile formFile);
    }
}
