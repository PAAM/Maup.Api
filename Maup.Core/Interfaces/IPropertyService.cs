using Maup.Core.Entities;
using Maup.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Core.Interfaces
{
    public interface IPropertyService
    {
        Task CreateProperty(Property property);
        PageList<Property> GetPropertiesAsync(PropertyFilter filter);
        Task<Property> GetProperty(int IdProperty);
        Task<bool> UpdateProperty(Property property);
        Task<bool> UpdatePropertyPrice(Property property);
    }
}
