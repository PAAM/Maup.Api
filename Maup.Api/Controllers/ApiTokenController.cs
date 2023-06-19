using Maup.Api.Responses;
using Maup.Core.DTO;
using Maup.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
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

        private readonly IConfiguration _configuration;
        public ApiTokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        [SwaggerOperation(
            Summary = "[Summary]: Generate a new token",
            Description = "[Description]: This End-Point will create a new Property, just you need to build a request with the correct parameters.",
            OperationId = "Auth"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult Auth(Login login)
        {
            if (IsValidLogin(login))
            {
                var token = GenerateToken();
                return Ok(new { token });
            }
            return NotFound();
        }

        private bool IsValidLogin(Login login)
        {
            return true;
        }

        private string GenerateToken()
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Secret"]));
            var siginCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(siginCredentials);


            var claims = new[] {
                new Claim(ClaimTypes.Name, "Pedro Aguirre"),
                new Claim(ClaimTypes.Email, "pedroa.aguirre@hotmail.com"),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(2)
            );

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
