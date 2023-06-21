using Maup.Core.DTO;
using Maup.Core.Entities;
using Maup.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Maup.Core.Services
{
    public class PropertyImageService : IPropertyImageService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IFileStore _fileStore;
        public PropertyImageService(IUnitOfWork unitOfWork, IFileStore fileStore)
        {
            _unitOfWork = unitOfWork;
            _fileStore = fileStore;
        }
        public async Task<PropertyImage> InsertNewPropertyImage(PropertyImage propertyImage, IFormFile formFile)
        {
            if (formFile != null)
            {
                var path = await _fileStore.SaveFile(formFile);
                propertyImage.File = path;
            }

            await _unitOfWork.PropertyImageRepository.Add(propertyImage);
            await _unitOfWork.SaveChangesAsync();
            return propertyImage;
        }


    }
}
