using System.ComponentModel.DataAnnotations;
namespace OAS_ClassLib.Models
{

    public class Product
    {
        public int ProductID { get; set; }
        public int SellerID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal StartPrice { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }

}
