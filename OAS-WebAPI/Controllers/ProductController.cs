using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAS_ClassLib.Models;
using OAS_ClassLib.Repositories;

namespace OAS_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class ProductController : ControllerBase
    {

        private readonly ProductServices _productServices;

        public ProductController(ProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllProduct()
        {
            var obj = _productServices.GetAllProducts();
            return Ok(obj);
        }

        [HttpDelete("{productId}")]

        public IActionResult RemoveProduct(int productId)
        {
            _productServices.RemoveProduct(productId);
            return Ok();
        }

        [HttpPatch("{productId}")]
        [Authorize(Roles = "User")]
        public IActionResult UpdateExisting(int productId, [FromBody] Product product)
        {
            _productServices.UpdateProduct(product);
            return Ok();
        }


        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult AddNewProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalid request");
            }
            _productServices.AddProduct(product);
            return Ok();
        }
        // Image Handling Endpoints
        [HttpPost("{productId}/uploadImage")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UploadImage(int productId, IFormFile image)
        {
            var filePath = await _productServices.UploadImageAsync(image, productId);

            if (filePath == null)
            {
                return BadRequest("No image file provided.");
            }

            return Ok(new { FilePath = filePath });
        }

        [HttpGet("downloadImage/{fileName}")]

        public IActionResult DownloadImage(string fileName)
        {
            var image = _productServices.DownloadImage(fileName);

            if (image == null)
            {
                return NotFound("Image not found.");
            }

            return File(image, "image/jpeg");
        }

        [HttpGet("{productId}/images")]
        [Authorize(Roles = "User")] 
        public IActionResult GetImagesByProductId(int productId)
        {
            var images = _productServices.GetImagesByProductId(productId);

            if (images == null || !images.Any())
            {
                return NotFound("No images found for this product.");
            }

            return Ok(images);
        }
    }
}
