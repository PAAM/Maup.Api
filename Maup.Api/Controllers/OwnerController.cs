using AutoMapper;
using Maup.Api.Responses;
using Maup.Core.DTO;
using Maup.Core.Entities;
using Maup.Core.Interfaces;
using Maup.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Maup.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService, IMapper mapper)
        {
            _mapper = mapper;
            _ownerService = ownerService;
        }

        [HttpPost]
        [SwaggerOperation(
         Summary = "[Summary]: Insert a new Owner",
         Description = "[Description]: This End-Point will create a new Owner, just you need to build a request with the correct parameters.</br><b>The Request does not require the ID parameter</b>",
         OperationId = "CreateOwner"
         )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ApiResponse<OwnerDto>))]
        public async Task<IActionResult> CreateOwner([FromForm] OwnerDto ownerDto)
        {
            var owner = _mapper.Map<Owner>(ownerDto);
            await _ownerService.CreateOwner(owner, ownerDto.Photo);
            ownerDto = _mapper.Map<OwnerDto>(owner);
            var response = new ApiResponse<OwnerDto>(ownerDto);

            return Ok(response);
        }

    }
}
