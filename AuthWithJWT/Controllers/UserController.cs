using BAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modal;

namespace AuthWithJWT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public IActionResult LoginUser(User obj)
        {
            try
            {
                var result = _userService.Login(obj);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetUser()
        {
            try
            {
                return Ok("result");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("secure-data")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult GetSecureData()
        {
            // Retrieve the username from the claims
            var username = User.Identity?.Name; // This gets the ClaimTypes.Name from the token

            if (string.IsNullOrEmpty(username) || username!="test")
            {
                return Unauthorized("User is not authenticated");
            }

            // Example of accessing other claims if needed
            var claims = User.Claims;
            var uniqueNameClaim = claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;

            // Return some data with the username
            var data = new
            {
                Message = "This is secured data",
                Username = username,
                UniqueName = uniqueNameClaim
            };

            return Ok(data);
        }

    }
}
