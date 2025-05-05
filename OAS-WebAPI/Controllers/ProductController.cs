using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        //[Authorize(Roles = "Admin")]
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
        public IActionResult UpdateExisting(int productId, [FromBody] Product product)
        {
            _crudService.UpdateProduct(product);
            return Ok();
        }


        [HttpPost]
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
        [HttpPost("{productId}/uploadImages")]
        public async Task<IActionResult> UploadImages(int productId, [FromForm] List<IFormFile> images)
        {
            var product = _crudService.GetProductById(productId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            if (images == null || images.Count == 0)
            {
                return BadRequest("No images provided.");
            }

            var filePaths = await _imageService.UploadImagesAsync(images, productId);

            return Ok(new { product, filePaths });
        }

        [HttpGet("{productId}/downloadImages")]
        public IActionResult DownloadImagesByProductId(int productId)
        {
            var images = _imageService.DownloadImagesByProductId(productId);

            if (images == null || images.Count == 0)
            {
                return NotFound("No images found for this product.");
            }

            var zipStream = new MemoryStream();
            using (var archive = new System.IO.Compression.ZipArchive(zipStream, System.IO.Compression.ZipArchiveMode.Create, true))
            {
                foreach (var image in images)
                {
                    var entry = archive.CreateEntry(Path.GetFileName(image.Name));
                    using (var entryStream = entry.Open())
                    {
                        image.CopyTo(entryStream);
                    }
                }
            }
            zipStream.Position = 0;

            return File(zipStream, "application/zip", $"Product_{productId}_Images.zip");
        }

        [HttpGet("{productId}/images")]
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