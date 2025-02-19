using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoperBackend.Services;

namespace ShoperBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService, ILogger<ProductsController> logger) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        private readonly ILogger<ProductsController> _logger = logger;

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query) || query.Length < 3)
            {
                return BadRequest("Query must be at least 3 characters long.");
            }

            try
            {
                var results = await _productService.SearchProductsAsync(query);
                if (!results.Any())
                    return NotFound("No products found.");

                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred in search.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
