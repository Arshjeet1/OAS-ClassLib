using OAS_ClassLib.Models;

namespace OAS_ClassLib.Repositories
{
    public class BidServices
    {
        public void AddBid(Bid newBid)
        {
            using (var context = new AppDbContext())
            {
                context.Bids.Add(newBid);
                context.SaveChanges();
            }
        }

    }
}
