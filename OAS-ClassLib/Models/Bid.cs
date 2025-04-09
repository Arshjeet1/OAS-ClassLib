using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace OAS_ClassLib.Models
{
    public class Bid
    {
        [Key]
        public int BidID { get; set; }
        [ForeignKey("Auction")]
        public int AuctionID { get; set; }
        [ForeignKey("User")]
        public int BuyerID { get; set; }
        [Required(ErrorMessage = "Please enter the value for amount.")]
        public decimal Amount { get; set; }
        public DateTime BidTime { get; set; }
    }
}
