using Microsoft.AspNetCore.Mvc;
using ShoperBackend.Services;

namespace ShoperBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query) || query.Length < 3)
            {
                return BadRequest("Query must be at least 3 characters long.");
            }

            var products = await _productService.SearchProductsAsync(query);
            return Ok(products);
        }
    }
}
