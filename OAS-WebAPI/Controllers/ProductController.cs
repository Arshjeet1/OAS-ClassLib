using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAS_ClassLib.Interfaces;
using OAS_ClassLib.Models;
using OAS_ClassLib.Repositories;

namespace OAS_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class ProductController : ControllerBase
    {

        //private readonly ProductServices _productServices;

        //public ProductController(ProductServices productServices)
        //{
        //    _productServices = productServices;
        //}
        private readonly IProductCrudService _crudService;
        private readonly IProductImageService _imageService;
        private readonly IProductStatisticsService _statisticsService;
        private readonly IProductQueryService _queryService;

        // Constructor Injection
        public ProductController(
            IProductCrudService crudService,
            IProductImageService imageService,
            IProductStatisticsService statisticsService,
            IProductQueryService queryService)
        {
            _crudService = crudService;
            _imageService = imageService;
            _statisticsService = statisticsService;
            _queryService = queryService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllProduct()
        {
            var obj = _crudService.GetAllProducts();
            return Ok(obj);
        }

        [HttpDelete("{productId}")]

        public IActionResult RemoveProduct(int productId)
        {
            _crudService.RemoveProduct(productId);
            return Ok();
        }

        [HttpPatch("{productId}")]
        [Authorize(Roles = "User")]
        public IActionResult UpdateExisting(int productId, [FromBody] Product product)
        {
            _crudService.UpdateProduct(product);
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
            _crudService.AddProduct(product);
            return Ok();
        }
        // Image Handling Endpoints
        [HttpPost("{productId}/uploadImage")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UploadImage(int productId, IFormFile image)
        {
            var filePath = await _imageService.UploadImageAsync(image, productId);

            if (filePath == null)
            {
                return BadRequest("No image file provided.");
            }

            return Ok(new { FilePath = filePath });
        }

        [HttpGet("downloadImage/{fileName}")]

        public IActionResult DownloadImage(string fileName)
        {
            var image = _imageService.DownloadImage(fileName);

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
            var images = _imageService.GetImagesByProductId(productId);

            if (images == null || !images.Any())
            {
                return NotFound("No images found for this product.");
            }

            return Ok(images);
        }
        // Aggregate functions endpoints
        [HttpGet("count")]
        public IActionResult GetProductCount()
        {
            var count = _statisticsService.GetProductCount();
            return Ok(count);
        }

        [HttpGet("totalStartPrice")]
        public IActionResult GetTotalStartPrice()
        {
            var totalStartPrice = _statisticsService.GetTotalStartPrice();
            return Ok(totalStartPrice);
        }

        [HttpGet("averageStartPrice")]
        public IActionResult GetAverageStartPrice()
        {
            var averageStartPrice = _statisticsService.GetAverageStartPrice();
            return Ok(averageStartPrice);
        }

        [HttpGet("minStartPrice")]
        public IActionResult GetMinStartPrice()
        {
            var minStartPrice = _statisticsService.GetMinStartPrice();
            return Ok(minStartPrice);
        }

        [HttpGet("maxStartPrice")]
        public IActionResult GetMaxStartPrice()
        {
            var maxStartPrice = _statisticsService.GetMaxStartPrice();
            return Ok(maxStartPrice);
        }

        [HttpGet("productsByCategory")]
        public IActionResult GetProductsByCategory()
        {
            var productsByCategory = _queryService.GetProductsByCategory();
            return Ok(productsByCategory);
        }

        [HttpGet("distinctCategories")]
        public IActionResult GetDistinctCategories()
        {
            var distinctCategories = _queryService.GetDistinctCategories();
            return Ok(distinctCategories);
        }

        [HttpGet("orderedByStartPrice")]
        public IActionResult GetProductsOrderedByStartPrice()
        {
            var orderedProducts = _queryService.GetProductsOrderedByStartPrice();
            return Ok(orderedProducts);
        }

        [HttpGet("orderedByStartPriceDesc")]
        public IActionResult GetProductsOrderedByStartPriceDesc()
        {
            var orderedProductsDesc = _queryService.GetProductsOrderedByStartPriceDesc();
            return Ok(orderedProductsDesc);
        }

    }
}
