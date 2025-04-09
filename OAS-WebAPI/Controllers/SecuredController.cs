using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OAS_ClassLib.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SecuredController : ControllerBase
    {
        private readonly UserServices _userService;

        public SecuredController(UserServices userService)
        {
            _userService = userService;
        }

        //[HttpGet("securedendpoint")]
        //public IActionResult GetSecureData()
        //{
        //    return Ok("This is a secure endpoint. Only authorized users can access this.");
        //}

        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetSignedData()
        {
            // Extract claims from the payload
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var name = User.FindFirst(ClaimTypes.Name)?.Value;
            var accessLevel = User.FindFirst("accessLevel")?.Value;
            var tokenExpiryClaim = User.FindFirst(JwtRegisteredClaimNames.Exp)?.Value;

            // Convert token expiry to DateTime
            DateTime? tokenExpiry = null;
            if (long.TryParse(tokenExpiryClaim, out long expirySeconds))
            {
                tokenExpiry = DateTimeOffset.FromUnixTimeSeconds(expirySeconds).DateTime;
            }


            // Fetch Token ID
            var tokenID = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

            // Example of handling blacklisted tokens (commented out)
            // if (IsTokenBlacklisted(tokenID)) { 
            //    return Unauthorized();
            // }

            return Ok(new
            {
                Role = role,
                Name = name,
                AccessLevel = accessLevel,
                TokenExpiry = tokenExpiry,
                TokenID = tokenID
            });

        }
       

    }
}