using OAS_ClassLib.Interfaces;
using OAS_ClassLib.Models;

namespace OAS_ClassLib.Repositories
{
    public class BidServices : IBidService, IBidStatisticsService, IBidGroupingService, IBidQueryService
    {
        public void AddBid(Bid newBid)
        {
            using (var context = new AppDbContext())
            {
                context.Bids.Add(newBid);
                context.SaveChanges();
            }
        }

        public decimal GetTotalBidsAmount(int auctionId)
        {
            using (var context = new AppDbContext())
            {
                return context.Bids
                              .Where(b => b.AuctionID == auctionId)
                              .Sum(b => b.Amount);
            }
        }

        public int GetBidCount(int auctionId)
        {
            using (var context = new AppDbContext())
            {
                return context.Bids
                              .Count(b => b.AuctionID == auctionId);
            }
        }

        public decimal GetAverageBidAmount(int auctionId)
        {
            using (var context = new AppDbContext())
            {
                return context.Bids
                              .Where(b => b.AuctionID == auctionId)
                              .Average(b => b.Amount);
            }
        }

        public decimal GetMinBidAmount(int auctionId)
        {
            using (var context = new AppDbContext())
            {
                return context.Bids
                              .Where(b => b.AuctionID == auctionId)
                              .Min(b => b.Amount);
            }
        }

        public decimal GetMaxBidAmount(int auctionId)
        {
            using (var context = new AppDbContext())
            {
                return context.Bids
                              .Where(b => b.AuctionID == auctionId)
                              .Max(b => b.Amount);
            }
        }

        public List<object> GetBidsByBuyer(int auctionId)
        {
            using (var context = new AppDbContext())
            {
                return context.Bids
                              .Where(b => b.AuctionID == auctionId)
                              .GroupBy(b => b.BuyerID)
                              .Select(g => new { BuyerID = g.Key, Count = g.Count() })
                              .ToList<object>();
            }
        }
        public List<int> GetUsersByBidDate(DateTime date)
        {
            using (var context = new AppDbContext())
            {
                return context.Bids
                              .Where(b => b.BidTime.Date == date.Date)
                              .Select(b => b.BuyerID)
                              .Distinct()
                              .ToList();
            }
        }

        public List<int> GetDistinctBuyers(int auctionId)
        {
            using (var context = new AppDbContext())
            {
                return context.Bids
                              .Where(b => b.AuctionID == auctionId)
                              .Select(b => b.BuyerID)
                              .Distinct()
                              .ToList();
            }
        }

        public List<Bid> GetBidsOrderedByAmount(int auctionId)
        {
            using (var context = new AppDbContext())
            {
                return context.Bids
                              .Where(b => b.AuctionID == auctionId)
                              .OrderBy(b => b.Amount)
                              .ToList();
            }
        }

        public List<Bid> GetBidsOrderedByAmountDesc(int auctionId)
        {
            using (var context = new AppDbContext())
            {
                return context.Bids
                              .Where(b => b.AuctionID == auctionId)
                              .OrderByDescending(b => b.Amount)
                              .ToList();
            }
        }
    }
}
