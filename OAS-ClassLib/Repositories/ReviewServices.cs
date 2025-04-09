using OAS_ClassLib.Models;


namespace OAS_ClassLib.Repositories
{
    public class ReviewServices
    {
        public List<Review> GetallReview()
        {
            using (var context = new AppDbContext())
            {
                return context.Reviews.ToList();
            }
        }

        public void AddReview(Review newReview)
        {
            using (var context = new AppDbContext())
            {
                context.Reviews.Add(newReview);
                context.SaveChanges();
            }
        }

        public void UpdateReview(Review updatedReview)
        {
            using (var context = new AppDbContext())
            {
                var existingReview = context.Reviews.Find(updatedReview.ReviewID);
                if (existingReview != null)
                {
                    existingReview.Comment = updatedReview.Comment;
                    existingReview.Rating = updatedReview.Rating;
                    context.SaveChanges();
                }
            }
        }

        public void DeleteReview(int reviewId)
        {
            using (var context = new AppDbContext())
            {
                var reviewToDelete = context.Reviews.Find(reviewId);
                if (reviewToDelete != null)
                {
                    context.Reviews.Remove(reviewToDelete);
                    context.SaveChanges();
                }
            }
        }
    }
}
