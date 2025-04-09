using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using OAS_ClassLib.Interfaces;
using OAS_ClassLib.Models;
using OAS_ClassLib.Repositories;

namespace OAS_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        //private readonly BidServices _bidServices;

        //public BidController()
        //{
        //    _bidServices = new BidServices();
        //}
        // Constructor injection
        private readonly IBidService _bidServices;
        private readonly IBidStatisticsService _bidStatisticsService;
        private readonly IBidGroupingService _bidGroupingService;
        private readonly IBidQueryService _bidQueryService;
        public BidController(IBidService bidServices, IBidStatisticsService bidStatisticsService,
                         IBidGroupingService bidGroupingService,
                         IBidQueryService bidQueryService)
        {
            _bidServices = bidServices ?? throw new ArgumentNullException(nameof(bidServices));
            _bidStatisticsService = bidStatisticsService ?? throw new ArgumentNullException(nameof(bidStatisticsService));
            _bidGroupingService = bidGroupingService ?? throw new ArgumentNullException(nameof(bidGroupingService));
            _bidQueryService = bidQueryService ?? throw new ArgumentNullException(nameof(bidQueryService));
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult AddBid([FromBody] Bid newBid)
        {
            _bidServices.AddBid(newBid);
            return Ok();
        }

        [HttpGet("total/{auctionId}")]
        public ActionResult<decimal> GetTotalBidsAmount(int auctionId)
        {
            var totalAmount = _bidStatisticsService.GetTotalBidsAmount(auctionId);
            return Ok(totalAmount);
        }

        [HttpGet("count/{auctionId}")]
        public ActionResult<int> GetBidCount(int auctionId)
        {
            var count = _bidStatisticsService.GetBidCount(auctionId);
            return Ok(count);
        }

        [HttpGet("average/{auctionId}")]
        public ActionResult<decimal> GetAverageBidAmount(int auctionId)
        {
            var averageAmount = _bidStatisticsService.GetAverageBidAmount(auctionId);
            return Ok(averageAmount);
        }

        [HttpGet("min/{auctionId}")]
        public ActionResult<decimal> GetMinBidAmount(int auctionId)
        {
            var minAmount = _bidStatisticsService.GetMinBidAmount(auctionId);
            return Ok(minAmount);
        }

        [HttpGet("max/{auctionId}")]
        public ActionResult<decimal> GetMaxBidAmount(int auctionId)
        {
            var maxAmount = _bidStatisticsService.GetMaxBidAmount(auctionId);
            return Ok(maxAmount);
        }

        [HttpGet("buyers/{auctionId}")]
        public ActionResult<List<object>> GetBidsByBuyer(int auctionId)
        {
            var bidsByBuyer = _bidGroupingService.GetBidsByBuyer(auctionId);
            return Ok(bidsByBuyer);
        }

        [HttpGet("distinct-buyers/{auctionId}")]
        public ActionResult<List<int>> GetDistinctBuyers(int auctionId)
        {
            var distinctBuyers = _bidGroupingService.GetDistinctBuyers(auctionId);
            return Ok(distinctBuyers);
        }

        [HttpGet("ordered/{auctionId}")]
        public ActionResult<List<Bid>> GetBidsOrderedByAmount(int auctionId)
        {
            var orderedBids = _bidQueryService.GetBidsOrderedByAmount(auctionId);
            return Ok(orderedBids);
        }

        [HttpGet("ordered-desc/{auctionId}")]
        public ActionResult<List<Bid>> GetBidsOrderedByAmountDesc(int auctionId)
        {
            var orderedBidsDesc = _bidQueryService.GetBidsOrderedByAmountDesc(auctionId);
            return Ok(orderedBidsDesc);
        }
        [HttpGet("users-by-date")]
        public ActionResult<List<int>> GetUsersByBidDate([FromQuery] DateTime date)
        {
            var users = _bidQueryService.GetUsersByBidDate(date);
            return Ok(users);
        }
    }
}
