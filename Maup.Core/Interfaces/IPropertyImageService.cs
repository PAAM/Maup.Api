using Maup.Core.DTO;
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
    public interface IPropertyImageService
    {
        Task<PropertyImage> InsertNewPropertyImage(PropertyImage propertyImage, IFormFile formFile);

        //PageList<PropertyImage> GetPropertiesAsync(PropertyFilter filter);

    }
}
