using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using OAS_ClassLib.Models;
using OAS_ClassLib.Repositories;

namespace OAS_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly BidServices _bidServices;

        public BidController()
        {
            _bidServices = new BidServices();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult AddBid([FromBody] Bid newBid)
        {
            _bidServices.AddBid(newBid);
            return Ok();
        }
    }
}
