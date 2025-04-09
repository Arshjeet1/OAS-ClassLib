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
    }
}
