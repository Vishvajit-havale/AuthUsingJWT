using BAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Modal;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthWithJWT.Controllers
{
    public class LoginControllers : Controller
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public LoginControllers(IAuthService authService, IConfiguration config)
        {
            _authService = authService;
            _configuration = config;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login(UserLoginModel userLogin)
        {
            try
            {
                // Dummy authentication logic - replace with actual authentication
                if ((userLogin.Username != "test" && userLogin.Username!="test1") || userLogin.Password != "password")
                    return Unauthorized();

                // Generate JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var keyString = _configuration.GetValue<string>("JWT:Secret");
                if (string.IsNullOrEmpty(keyString))
                    throw new InvalidOperationException("JWT key is not configured properly.");


                var key = Encoding.UTF8.GetBytes(keyString);

                // Ensure the key is long enough (256-bit key for HMACSHA256)
                if (key.Length < 32)
                    return StatusCode(500, "JWT secret key is too short");

                var identity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, userLogin.Username == "test" ? "Admin" : "User" ),
                    new Claim(ClaimTypes.Name, $"{userLogin.Username}"),

                });
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = identity,
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = _configuration["JWT:Issuer"],  // Add Issuer
                    Audience = _configuration["JWT:Audience"],  // Add Audience
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString });
            }
            catch (Exception ex)
            {
                // Better to return 500 for server errors
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
