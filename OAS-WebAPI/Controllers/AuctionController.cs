using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAS_ClassLib.Interfaces;
using OAS_ClassLib.Models;
using OAS_ClassLib.Repositories;

namespace OAS_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        //private readonly AuctionServices _AuctionServices;

        //public AuctionController(AuctionServices auctionServices)
        //{
        //    _AuctionServices = auctionServices;
        //}
        private readonly IAuctionService _AuctionServices;

        public AuctionController(IAuctionService auctionServices)
        {
            _AuctionServices = auctionServices;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllAuctionDetails()
        {
            var auction = _AuctionServices.GetAllAuctions();
            return Ok(auction);
        }

        [HttpPost]  
        public IActionResult AddNewAuction([FromBody] Auction auction)
        {
            if (auction == null)
            {
                return BadRequest("Invalid Request");
            }
            int obj = _AuctionServices.InsertAuction(auction);
            return Ok(obj);
        }
        [HttpPatch]
        public IActionResult UpdateNewAuction([FromBody] Auction auction)
        {
            if (auction == null)
            {
                return BadRequest("Invalid Request");
            }
            int obj = _AuctionServices.UpdateAuction(auction);
            return Ok(obj);
        }

        [HttpDelete("{auctionId}")]
        public IActionResult DeleteNewAuction(int auctionId)
        {
            if (auctionId <= 0)
            {
                return BadRequest("Invalid AuctionId.");
            }

            int result = _AuctionServices.DeleteAuction(auctionId);
            if (result > 0)
            {
                return Ok($"Auction with AuctionId {auctionId} deleted successfully.");
            }
            else
            {
                return NotFound($"Auction with AuctionId {auctionId} does not exist.");
            }
        }
    }
}
