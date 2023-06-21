using AutoMapper;
using Maup.Api.Responses;
using Maup.Core.DTO;
using Maup.Core.Entities;
using Maup.Core.Filters;
using Maup.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Reflection.Metadata;

namespace Maup.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService, IMapper mapper)
        {
            _mapper = mapper;
            _propertyService = propertyService;

        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "[Summary]: Insert a new Property",
            Description = "[Description]: This End-Point will create a new Property, just you need to build a request with the correct parameters.</br><b>The Request does not require the ID parameter</b>",
            OperationId = "CreateProperty"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ApiResponse<PropertyDto>))]
        public async Task<IActionResult> CreateProperty(PropertyDto propertyDto)
        {
            var property = _mapper.Map<Property>(propertyDto);
            await _propertyService.CreateProperty(property);
            propertyDto = _mapper.Map<PropertyDto>(property);
            var response = new ApiResponse<PropertyDto>(propertyDto);

            return Ok(response);
        }


        [HttpGet]
        [SwaggerOperation(
            Summary = "[Summary]: List of Properties",
            Description = "[Description]: This End-Point returns a list with all properties on the DataBase.",
            OperationId = "GetProperties"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<PropertyDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<IEnumerable<PropertyDto>>))]
        public IActionResult GetProperties([FromQuery] PropertyFilter filter)
        {
            var properties = _propertyService.GetPropertiesAsync(filter);
            var propertyDto = _mapper.Map<IEnumerable<PropertyDto>>(properties);
            var response = new ApiResponse<IEnumerable<PropertyDto>>(propertyDto);

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
            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }



        [HttpGet("{IdProperty}")]
        [SwaggerOperation(
            Summary = "[Summary]: Property by IdProperty",
            Description = "[Description]: This End-Point returns only-one property available for the Id sent on the request.",
            OperationId = "GetProperty"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ApiResponse<PropertyDto>))]
        public async Task<IActionResult> GetProperty(int IdProperty)
        {
            var properties = await _propertyService.GetProperty(IdProperty);
            var propertyDto = _mapper.Map<PropertyDto>(properties);
            var response = new ApiResponse<PropertyDto>(propertyDto);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProperty(PropertyDto propertyDto)
        {
            var property = _mapper.Map<Property>(propertyDto);
            await _propertyService.UpdateProperty(property);

            return Ok(property);
        }

        //[HttpPut("{IdProperty}")]
        //public async Task<IActionResult> UpdatePropertyPrice(int IdProperty, PropertyPriceDto propertyPriceDto)
        //{
        //    propertyPriceDto.IdProperty = IdProperty;
        //    var property = _mapper.Map<Property>(propertyPriceDto);
        //    var result = await _propertyService.UpdatePropertyPrice(property);
        //    var response = new ApiResponse<bool>(result);

        //    return Ok(response);
        //}
    }
}
