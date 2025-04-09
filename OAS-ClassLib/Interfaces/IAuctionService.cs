using OAS_ClassLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS_ClassLib.Interfaces
{
    public interface IAuctionService
    {
        int InsertAuction(Auction auction);
        int UpdateAuction(Auction auction);
        int DeleteAuction(int auctionId);
        List<Auction> GetAllAuctions();
    }

}

