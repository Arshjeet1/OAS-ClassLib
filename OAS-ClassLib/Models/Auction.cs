using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS_ClassLib.Models
{
    public class Auction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionId { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string? CurrentBid { get; set; }
        public string Status { get; set; }

        // Navigation properties
        public Product? Product { get; set; }
        public ICollection<Bid>? Bids { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
    }
}
