using System.ComponentModel.DataAnnotations;
namespace OAS_ClassLib.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }

        [Required]
        public int BuyerID { get; set; }

        [Required]
        public int AuctionID { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string? PaymentStatus { get; set; }

        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }
    }
}
