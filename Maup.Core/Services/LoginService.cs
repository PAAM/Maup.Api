using Maup.Core.Entities;
using Maup.Core.Exceptions;
using Maup.Core.Interfaces;
using Maup.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Core.Services
{
    public class LoginService : ILoginService
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IConfiguration _configuration;
        public LoginService(IConfiguration configuration, IOwnerRepository ownerRepository)
        {
            _configuration = configuration;
            _ownerRepository = ownerRepository;
        }
        public async Task<string> IsValidLogin(Login login)
        {
            var owner = new Owner() { User = login.User, Password = login.Password };
            var user = await _ownerRepository.GetLoginByCredentials(owner);
            if (user != null)
            {
                return GenerateToken(user);
            }
            else
            {
                throw new CustomException("Login failed. Please check the credentials.");
            }
        }

        private string GenerateToken(Owner owner)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Secret"]));
            var siginCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(siginCredentials);


            var claims = new[] {
                new Claim(ClaimTypes.Name, owner.Name),
                new Claim(ClaimTypes.Email, owner.User),
                new Claim(ClaimTypes.Role,owner.Rol.ToString()),
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
