using JWT.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OAS_ClassLib.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] string username, [FromForm] string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Invalid credentials");
            }

            // For demonstration purposes, we'll create a User object with hardcoded values.
            // In a real application, you would validate the username and password against a database
            // and retrieve the user details accordingly.
            var user = new User
            {
                UserId = 1, // This should be retrieved from the database
                Name = username,
                Password = password, // This should be hashed and validated
                Email = "user@example.com", // This should be retrieved from the database
                Role = "Admin", // This should be retrieved from the database
                ContactNumber = "1234567890" // This should be retrieved from the database
            };

            // Generate JWT Token
            TokenGeneration jwtTokenString = new TokenGeneration(_configuration);
            string tokenString = jwtTokenString.GenerateJWT(user);

            return Ok(new { Token = tokenString });
        }
    }
}