using OAS_ClassLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
