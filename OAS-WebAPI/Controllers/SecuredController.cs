using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SecuredController : ControllerBase
    {
        [HttpGet("securedendpoint")]
        public IActionResult GetSecureData()
        {
            return Ok("This is a secure endpoint. Only authorized users can access this.");
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetSignedData()
        {
            // Extract claims from the payload
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var departmentName = User.FindFirst("Department")?.Value;
            var accessLevel = User.FindFirst("accessLevel")?.Value;
            var tokenExpiryClaim = User.FindFirst(JwtRegisteredClaimNames.Exp)?.Value;

            // Convert token expiry to DateTime
            DateTime? tokenExpiry = null;
            if (long.TryParse(tokenExpiryClaim, out long expirySeconds))
            {
                tokenExpiry = DateTimeOffset.FromUnixTimeSeconds(expirySeconds).DateTime;
            }

            // Validate extracted values
            if (departmentName == "Finance")
            {
                // Perform actions specific to the Finance department
            }
            else
            {
                // Perform other actions
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
                Department = departmentName,
                AccessLevel = accessLevel,
                TokenExpiry = tokenExpiry,
                TokenID = tokenID
            });
        }
    }
}