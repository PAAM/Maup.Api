using AutoMapper;
using Maup.Api.Responses;
using Maup.Core.DTO;
using Maup.Core.Entities;
using Maup.Core.Filters;
using Maup.Core.Interfaces;
using Maup.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Maup.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyImageController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IPropertyImageService _propertyImageService;
        public PropertyImageController(IMapper mapper, IPropertyImageService propertyImageService)
        {
            _mapper = mapper;
            _propertyImageService = propertyImageService;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "[Summary]: Insert a new Property Image",
            Description = "[Description]: This End-Point will insert a new Property Image, just you need to build a request with the correct parameters.</br><b>The Request does not require the ID parameter</b>",
            OperationId = "InsertNewPropertyImage"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ApiResponse<PropertyImageDto>))]
        public async Task<IActionResult> NewPropertyImage([FromForm] PropertyImageDto propertyImageDto)
        {
            var property = _mapper.Map<PropertyImage>(propertyImageDto);
            await _propertyImageService.InsertNewPropertyImage(property, propertyImageDto.File);
            propertyImageDto = _mapper.Map<PropertyImageDto>(property);
            var response = new ApiResponse<PropertyImageDto>(propertyImageDto);
            return Ok(response);
        }


        [HttpGet]
        [HttpGet]
        [SwaggerOperation(
            Summary = "[Summary]: List of images from properties",
            Description = "[Description]: This End-Point returns a list with all images on the DataBase.",
            OperationId = "GetPropertyImages"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<PropertyImageDto>>))]
        public IActionResult GetPropertyImages([FromQuery] PropertyImageFilter filter)
        {
            var properties = _propertyImageService.GetPropertiesAsync(filter);
            var response = new ApiResponse<IEnumerable<PropertyImage>>(properties);

            var metadata = new Metadata
            {
                PageIndex = properties.PageIndex,
                TotalPages = properties.TotalPages,
                PageSize = properties.PageSize,
                TotalRows = properties.TotalRows,
                HasPreviousPage = properties.HasPreviousPage,
                HasNextPage = properties.HasNextPage,
                NextPageNumber = properties.NextPageNumber,
                PreviousPageNumber = properties.PreviousPageNumber
            };
            response.Meta = metadata;
            return Ok(response);
        }

    }
}
