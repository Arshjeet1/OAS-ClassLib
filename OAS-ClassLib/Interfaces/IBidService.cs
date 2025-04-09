using OAS_ClassLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS_ClassLib.Interfaces
{
    public interface IBidService
    {
        void AddBid(Bid bid);
    }
    public interface IBidStatisticsService
    {
        decimal GetTotalBidsAmount(int auctionId);
        int GetBidCount(int auctionId);
        decimal GetAverageBidAmount(int auctionId);
        decimal GetMinBidAmount(int auctionId);
        decimal GetMaxBidAmount(int auctionId);
    }
    public interface IBidGroupingService
    {
        List<object> GetBidsByBuyer(int auctionId);
        List<int> GetDistinctBuyers(int auctionId);
    }
    public interface IBidQueryService
    {
        List<int> GetUsersByBidDate(DateTime date);
        List<Bid> GetBidsOrderedByAmount(int auctionId);
        List<Bid> GetBidsOrderedByAmountDesc(int auctionId);
    }

}

