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
        public IActionResult UpdateExisting(int productId, [FromBody] Product product)
        {
            _productServices.UpdateProduct(product);
            return Ok();
        }


        [HttpPost]
        public IActionResult AddNewProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalid request");
            }
            _productServices.AddProduct(product);
            return Ok();
        }
        
    }
}
