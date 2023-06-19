using Maup.Core.Entities;
using Maup.Core.Filters;
using Maup.Core.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Core.Services
{
    public class PropertyService : IPropertyService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly Pagination _pagination;
        public PropertyService(IUnitOfWork unitOfWork, IOptions<Pagination> option)
        {
            _unitOfWork = unitOfWork;
            _pagination = option.Value;
        }

        public async Task CreateProperty(Property property)
        {

            await _unitOfWork.PropertyRepository.Add(property);
        }

        public PageList<Property> GetPropertiesAsync(PropertyFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _pagination.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _pagination.DefaultPageSize : filter.PageNumber;

            var properties = _unitOfWork.PropertyRepository.GetAll();

            if (filter.IdProperty != null)
            {
                properties = properties.Where(e => e.Id == filter.IdProperty);
            }

            if (filter.Name != null)
            {
                properties = properties.Where(e => e.Name.ToLower() == filter.Name.ToLower());
            }

            if (filter.Address != null)
            {
                properties = properties.Where(e => e.Address.ToLower() == filter.Address.ToLower());
            }

            if (filter.Price != null)
            {
                properties = properties.Where(e => e.Price == filter.Price);
            }

            if (filter.CodeInternal != null)
            {
                properties = properties.Where(e => e.CodeInternal == filter.CodeInternal);
            }

            if (filter.Year != null)
            {
                properties = properties.Where(e => e.Year == filter.Year);
            }

            if (filter.IdOwner != null)
            {
                properties = properties.Where(e => e.IdOwner == filter.IdOwner);
            }

            var pageList = PageList<Property>.Create(properties, filter.PageNumber, filter.PageSize);
            return pageList;
        }

        public async Task<Property> GetProperty(int IdProperty)
        {
            var response = await _unitOfWork.PropertyRepository.GetById(IdProperty);
            return response;
        }
        public async Task<bool> UpdateProperty(Property property)
        {
            _unitOfWork.PropertyRepository.Update(property);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePropertyPrice(Property property)
        {
            _unitOfWork.PropertyRepository.Update(property);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
