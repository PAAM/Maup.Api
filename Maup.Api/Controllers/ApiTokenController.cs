using Maup.Api.Responses;
using Maup.Core.DTO;
using Maup.Core.Entities;
using Maup.Core.Exceptions;
using Maup.Core.Interfaces;
using Maup.Core.Repositories;
using Maup.Infrastructure.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Maup.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiTokenController : ControllerBase
    {

        private readonly ILoginService _loginService;
        public ApiTokenController(IConfiguration configuration, ILoginService loginService)
        {
            _loginService = loginService;
        }


        [HttpPost]
        [SwaggerOperation(
            Summary = "[Summary]: Generate a new token",
            Description = "[Description]: This End-Point will generate token, just you need to build a request with the correct parameters.",
            OperationId = "Auth"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> Auth(Login login)
        {
            var token = await _loginService.IsValidLogin(login);
            if (token != null)
            {
                return Ok(new { token });
            }
            return BadRequest();
        }




    }
}
