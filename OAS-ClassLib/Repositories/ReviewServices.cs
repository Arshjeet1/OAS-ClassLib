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
        public int GetReviewCount(int targetUserId)
        {
            using (var context = new AppDbContext())
            {
                return context.Reviews
                              .Count(r => r.TargetUserID == targetUserId);
            }
        }

        public double GetAverageRating(int targetUserId)
        {
            using (var context = new AppDbContext())
            {
                return context.Reviews
                              .Where(r => r.TargetUserID == targetUserId)
                              .Average(r => r.Rating);
            }
        }

        public int GetMinRating(int targetUserId)
        {
            using (var context = new AppDbContext())
            {
                return context.Reviews
                              .Where(r => r.TargetUserID == targetUserId)
                              .Min(r => r.Rating);
            }
        }

        public int GetMaxRating(int targetUserId)
        {
            using (var context = new AppDbContext())
            {
                return context.Reviews
                              .Where(r => r.TargetUserID == targetUserId)
                              .Max(r => r.Rating);
            }
        }

        public List<object> GetReviewsByUser(int targetUserId)
        {
            using (var context = new AppDbContext())
            {
                return context.Reviews
                              .Where(r => r.TargetUserID == targetUserId)
                              .GroupBy(r => r.UserID)
                              .Select(g => new { UserID = g.Key, Count = g.Count() })
                              .ToList<object>();
            }
        }

        public List<int> GetDistinctUsers(int targetUserId)
        {
            using (var context = new AppDbContext())
            {
                return context.Reviews
                              .Where(r => r.TargetUserID == targetUserId)
                              .Select(r => r.UserID)
                              .Distinct()
                              .ToList();
            }
        }

        public List<Review> GetReviewsOrderedByRating(int targetUserId)
        {
            using (var context = new AppDbContext())
            {
                return context.Reviews
                              .Where(r => r.TargetUserID == targetUserId)
                              .OrderBy(r => r.Rating)
                              .ToList();
            }
        }
        public List<Review> GetReviewsOrderedByRatingDesc(int targetUserId)
        {
            using (var context = new AppDbContext())
            {
                return context.Reviews
                              .Where(r => r.TargetUserID == targetUserId)
                              .OrderByDescending(r => r.Rating)
                              .ToList();
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
