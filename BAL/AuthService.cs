using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Modal;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class AuthService:IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthResponse Authenticate(UserLoginModel userLogin)
        {
            if (userLogin.Username == "testuser" && userLogin.Password == "password")
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userLogin.Username)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _configuration["JWT:Issuer"], // Ensure these match
                    Audience = _configuration["JWT:Audience"] // Ensure these match
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new AuthResponse { Token = tokenHandler.WriteToken(token) };
            }

            return null;
        }

    }
}
