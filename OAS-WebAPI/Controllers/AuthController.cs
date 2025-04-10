using JWT.Logic;
using Microsoft.AspNetCore.Mvc;
using OAS_ClassLib.Interfaces;
using OAS_ClassLib.Models;
using OAS_ClassLib.Repositories;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserServices _userService;

        public AuthController(IConfiguration configuration, IUserServices userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] string username, [FromForm] string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Enter the credentials");
            }

            int valid = _userService.Validate(username,password);

            if (valid==-1)
            {
                return Unauthorized("Invalid username or password");
            }
            User user = _userService.GetUserByUsernameAndId(username,password);
            // Generate JWT Token
            TokenGeneration jwtTokenString = new TokenGeneration(_configuration);
            string tokenString = jwtTokenString.GenerateJWT(user);

            return Ok(new { Token = tokenString });
        }
    }
}