using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OAS_ClassLib.Models
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        public string ImageFileName { get; set; }

        public byte[] ImageData { get; set; }

    }
}
