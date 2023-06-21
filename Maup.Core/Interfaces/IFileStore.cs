using Maup.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Maup.Core.Interfaces
{
    public interface IFileStore
    {
        public Task<string> SaveFile(IFormFile formFile);
    }
}
