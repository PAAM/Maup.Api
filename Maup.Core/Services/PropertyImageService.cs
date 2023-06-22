using Maup.Core.Entities;
using Maup.Core.Exceptions;
using Maup.Core.Filters;
using Maup.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;


namespace Maup.Core.Services
{
    public class PropertyImageService : IPropertyImageService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IFileStore _fileStore;
        public readonly IPropertyService _propertyService;
        private readonly Pagination _pagination;

        public PropertyImageService(IUnitOfWork unitOfWork, IFileStore fileStore, IPropertyService propertyService, IOptions<Pagination> option)
        {
            _unitOfWork = unitOfWork;
            _fileStore = fileStore;
            _propertyService = propertyService;
            _pagination = option.Value;
        }

        public async Task<PropertyImage> InsertNewPropertyImage(PropertyImage propertyImage, IFormFile formFile)
        {
            var property = await _propertyService.GetProperty(propertyImage.IdProperty);
            if (property == null)
            {
                throw new CustomException("Insert property failed, please make sure property exists on database.");
            }


            if (formFile != null)
            {
                var path = await _fileStore.SaveFile(formFile);
                propertyImage.File = path;
            }
            else
            {
                throw new CustomException("Insert property failed, please make sure the attach an image.");
            }

            await _unitOfWork.PropertyImageRepository.Add(propertyImage);
            await _unitOfWork.SaveChangesAsync();
            return propertyImage;
        }

        public PageList<PropertyImage> GetPropertiesAsync(PropertyImageFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _pagination.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _pagination.DefaultPageSize : filter.PageNumber;

            var response = _unitOfWork.PropertyImageRepository.GetAll();

            if (filter.IdPropertyImage > 0)
            {
                response = response.Where(e => e.Id == filter.IdPropertyImage);
            }

            if (filter.IdProperty > 0)
            {
                response = response.Where(e => e.IdProperty == filter.IdProperty);
            }

            if (filter.Enabled != null)
            {
                response = response.Where(e => e.Enabled == filter.Enabled);
            }

            var pageList = PageList<PropertyImage>.Create(response, filter.PageNumber, filter.PageSize);
            return pageList;
        }



    }
}
