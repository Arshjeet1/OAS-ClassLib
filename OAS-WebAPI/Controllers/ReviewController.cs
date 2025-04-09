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
    public class ReviewController : ControllerBase
    {
        private readonly ReviewServices _reviewServices;

        public ReviewController()
        {
            _reviewServices = new ReviewServices();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<Review>> GetAllReviews()
        {
            return _reviewServices.GetallReview();
        }

        [HttpPost]

        public ActionResult AddReview([FromBody] Review newReview)
        {
            _reviewServices.AddReview(newReview);
            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateReview([FromBody] Review updatedReview)
        {
            _reviewServices.UpdateReview(updatedReview);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "User")]
        public ActionResult DeleteReview(int id)
        {
            _reviewServices.DeleteReview(id);
            return Ok();
        }

        [HttpGet("count/{targetUserId}")]
        public ActionResult<int> GetReviewCount(int targetUserId)
        {
            var count = _reviewServices.GetReviewCount(targetUserId);
            return Ok(count);
        }

        [HttpGet("average/{targetUserId}")]
        public ActionResult<double> GetAverageRating(int targetUserId)
        {
            var averageRating = _reviewServices.GetAverageRating(targetUserId);
            return Ok(averageRating);
        }

        [HttpGet("min/{targetUserId}")]
        public ActionResult<int> GetMinRating(int targetUserId)
        {
            var minRating = _reviewServices.GetMinRating(targetUserId);
            return Ok(minRating);
        }

        [HttpGet("max/{targetUserId}")]
        public ActionResult<int> GetMaxRating(int targetUserId)
        {
            var maxRating = _reviewServices.GetMaxRating(targetUserId);
            return Ok(maxRating);
        }

        [HttpGet("users/{targetUserId}")]
        public ActionResult<List<object>> GetReviewsByUser(int targetUserId)
        {
            var reviewsByUser = _reviewServices.GetReviewsByUser(targetUserId);
            return Ok(reviewsByUser);
        }

        [HttpGet("distinct-users/{targetUserId}")]
        public ActionResult<List<int>> GetDistinctUsers(int targetUserId)
        {
            var distinctUsers = _reviewServices.GetDistinctUsers(targetUserId);
            return Ok(distinctUsers);
        }

        [HttpGet("ordered/{targetUserId}")]
        public ActionResult<List<Review>> GetReviewsOrderedByRating(int targetUserId)
        {
            var orderedReviews = _reviewServices.GetReviewsOrderedByRating(targetUserId);
            return Ok(orderedReviews);
        }

        [HttpGet("ordered-desc/{targetUserId}")]
        public ActionResult<List<Review>> GetReviewsOrderedByRatingDesc(int targetUserId)
        {
            var orderedReviewsDesc = _reviewServices.GetReviewsOrderedByRatingDesc(targetUserId);
            return Ok(orderedReviewsDesc);
        }
    }
}
