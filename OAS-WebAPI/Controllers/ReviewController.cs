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
    public class ReviewController : ControllerBase
    {
        //private readonly ReviewServices _reviewServices;

        //public ReviewController()
        //{
        //    _reviewServices = new ReviewServices();
        //}
        private readonly IReviewInsertService _reviewInsertService;
        private readonly IReviewRetrieveService _reviewRetrieveService;
        private readonly IReviewDeleteService _reviewDeleteService;
        private readonly IReviewStatisticsService _statisticsService;
        private readonly IReviewQueryService _queryService;
        private readonly IReviewAnalysisService _analysisService;

        // Constructor Injection
        public ReviewController(IReviewInsertService reviewInsertService,
                                IReviewRetrieveService reviewRetrieveService,
                                IReviewDeleteService reviewDeleteService,IReviewStatisticsService statisticsService,
                                IReviewQueryService queryService,IReviewAnalysisService analysisService)
        {
            _reviewInsertService = reviewInsertService;
            _reviewRetrieveService = reviewRetrieveService;
            _reviewDeleteService = reviewDeleteService;
            _statisticsService = statisticsService;
            _queryService = queryService;
            _analysisService = analysisService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<Review>> GetAllReviews()
        {
            return _reviewRetrieveService.GetallReview();
        }

        [HttpPost]

        public ActionResult AddReview([FromBody] Review newReview)
        {
            _reviewInsertService.AddReview(newReview);
            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateReview([FromBody] Review updatedReview)
        {
            _reviewInsertService.UpdateReview(updatedReview);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteReview(int id)
        {
            _reviewDeleteService.DeleteReview(id);
            return Ok();
        }

        [HttpGet("count/{targetUserId}")]
        public ActionResult<int> GetReviewCount(int targetUserId)
        {
            var count = _statisticsService.GetReviewCount(targetUserId);
            return Ok(count);
        }

        [HttpGet("average/{targetUserId}")]
        public ActionResult<double> GetAverageRating(int targetUserId)
        {
            var averageRating = _statisticsService.GetAverageRating(targetUserId);
            return Ok(averageRating);
        }

        [HttpGet("min/{targetUserId}")]
        public ActionResult<int> GetMinRating(int targetUserId)
        {
            var minRating = _statisticsService.GetMinRating(targetUserId);
            return Ok(minRating);
        }

        [HttpGet("max/{targetUserId}")]
        public ActionResult<int> GetMaxRating(int targetUserId)
        {
            var maxRating = _statisticsService.GetMaxRating(targetUserId);
            return Ok(maxRating);
        }

        [HttpGet("users/{targetUserId}")]
        public ActionResult<List<object>> GetReviewsByUser(int targetUserId)
        {
            var reviewsByUser = _analysisService.GetReviewsByUser(targetUserId);
            return Ok(reviewsByUser);
        }

        [HttpGet("distinct-users/{targetUserId}")]
        public ActionResult<List<int>> GetDistinctUsers(int targetUserId)
        {
            var distinctUsers = _analysisService.GetDistinctUsers(targetUserId);
            return Ok(distinctUsers);
        }

        [HttpGet("ordered/{targetUserId}")]
        public ActionResult<List<Review>> GetReviewsOrderedByRating(int targetUserId)
        {
            var orderedReviews = _queryService.GetReviewsOrderedByRating(targetUserId);
            return Ok(orderedReviews);
        }

        [HttpGet("ordered-desc/{targetUserId}")]
        public ActionResult<List<Review>> GetReviewsOrderedByRatingDesc(int targetUserId)
        {
            var orderedReviewsDesc = _queryService.GetReviewsOrderedByRatingDesc(targetUserId);
            return Ok(orderedReviewsDesc);
        }
    }
}
