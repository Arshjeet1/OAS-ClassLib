using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAS_ClassLib.Models;
using OAS_ClassLib.Repositories;

namespace OAS_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userServices;

        public UserController(UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid request");
            }
            bool result = _userServices.AddUser(user);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userServices.GetUsers();
            return Ok(users);
        }
        [HttpPatch]
        public IActionResult UpdateNewAuction([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid Request");
            }
            bool obj = _userServices.UpdateUser(user);
            return Ok(obj);
        }

        [HttpDelete("{UserId}")]
        public IActionResult DeleteNewAuction(int UserId)
        {
            if (UserId <= 0)
            {
                return BadRequest("Invalid AuctionId.");
            }

            bool result = _userServices.DeleteUser(UserId);
            if (result)
            {
                return Ok($"Auction with AuctionId {UserId} deleted successfully.");
            }
            else
            {
                return NotFound($"Auction with AuctionId {UserId} does not exist.");
            }


        }
    }
}
