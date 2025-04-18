using Microsoft.Data.SqlClient;
using OAS_ClassLib.Interfaces;
using OAS_ClassLib.Models;
namespace OAS_ClassLib.Repositories
{
    public class AuctionServices : IAuctionService
    {
        DB1 dbObject = new DB1();


        #region Operations
        public int InsertAuction(Auction auction)
        {
            DB1.nameValuePairList nvpList = new DB1.nameValuePairList
            {
                new DB1.nameValuePair("@AuctionId", auction.AuctionId),
                new DB1.nameValuePair("@ProductId", auction.ProductId),
                new DB1.nameValuePair("@StartDate", auction.StartDate),
                new DB1.nameValuePair("@EndDate", auction.EndDate),
                new DB1.nameValuePair("@CurrentBid", auction.CurrentBid),
                new DB1.nameValuePair("@Status", auction.Status)
            };

            return dbObject.Insert(DB1.StoredProcedures.InsertAuction, nvpList);
        }

        public int UpdateAuction(Auction auction)
        {
            DB1.nameValuePairList nvpList = new DB1.nameValuePairList
            {
              
                new DB1.nameValuePair("@ProductId", auction.ProductId),
                new DB1.nameValuePair("@StartDate", auction.StartDate),
                new DB1.nameValuePair("@EndDate", auction.EndDate),
                new DB1.nameValuePair("@CurrentBid", auction.CurrentBid),
                new DB1.nameValuePair("@Status", auction.Status)
            };

            return dbObject.Update(DB1.StoredProcedures.UpdateAuction, nvpList);
        }

        public int DeleteAuction(int auctionId)
        {
            DB1.nameValuePairList nvpList = new DB1.nameValuePairList
            {
                new DB1.nameValuePair("@AuctionId", auctionId)
            };

            return dbObject.Delete(DB1.StoredProcedures.DeleteAuction, nvpList);
        }
        public List<Auction> GetAllAuctions()
        {
            List<Auction> auctions = new List<Auction>();
            DB1.nameValuePairList nvpList = new DB1.nameValuePairList();

            try
            {
                SqlDataReader reader = dbObject.GetDataReader(DB1.StoredProcedures.GetAllAuctions, nvpList);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Auction auction = new Auction
                        {
                            AuctionId = Convert.ToInt32(reader["AuctionId"]),
                            ProductId = Convert.ToInt32(reader["ProductId"]),
                            StartDate = Convert.ToDateTime(reader["StartDate"]),
                            EndDate = Convert.ToDateTime(reader["EndDate"]),
                            CurrentBid = reader["CurrentBid"].ToString(),
                            Status = reader["Status"].ToString()
                        };

                        auctions.Add(auction);
                    }

                    reader.Close();
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("Error fetching auction details: " + exp.Message);
            }

            return auctions;
        }

        #endregion
    }
}
