using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS_ClassLib.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        public int SellerID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal StartPrice { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Status { get; set; }

        // Navigation property
        public ICollection<ProductImage>? ProductImages { get; set; }
        public ICollection<Auction>? Auctions { get; set; }
    }
}
