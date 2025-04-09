using OAS_ClassLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS_ClassLib.Interfaces
{
    public interface IReviewInsertService
    {
        void AddReview(Review review);
        void UpdateReview(Review review);
    }

    public interface IReviewRetrieveService
    {
        List<Review> GetallReview();
    }

    public interface IReviewDeleteService
    {
        void DeleteReview(int reviewId);
    }
    public interface IReviewStatisticsService
    {
        int GetReviewCount(int targetUserId);
        double GetAverageRating(int targetUserId);
        int GetMinRating(int targetUserId);
        int GetMaxRating(int targetUserId);
    }
    public interface IReviewQueryService
    {
        List<Review> GetReviewsOrderedByRating(int targetUserId);
        List<Review> GetReviewsOrderedByRatingDesc(int targetUserId);
    }

    public interface IReviewAnalysisService
    {
        List<object> GetReviewsByUser(int targetUserId);
        List<int> GetDistinctUsers(int targetUserId);
    }

}

