using Maup.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Core.Interfaces
{
    public interface ILoginService
    {
        Task<string> IsValidLogin(Login login);
    }
}
