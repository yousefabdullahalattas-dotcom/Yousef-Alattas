using BootCamp_MVC_EF.Application.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp_MVC_EF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();

            return Ok(new { message = "Get all products", products });
        }


        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            return Ok(new { message = "Get product", product });
        }

    }
}
