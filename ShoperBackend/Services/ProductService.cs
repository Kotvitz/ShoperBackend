using ShoperBackend.Models;
using System.Text.Json;

namespace ShoperBackend.Services
{
    public class ProductService(HttpClient httpClient, IConfiguration configuration, ILogger<ProductService> logger) : IProductService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILogger<ProductService> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 3)
                return Enumerable.Empty<ProductDto>();

            var shopUrl = _configuration["Shoper:ShopUrl"]?.TrimEnd('/');
            var requestUri = $"{shopUrl}/webapi/rest/products";

            try
            {
                var response = await _httpClient.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var shopResponse = JsonSerializer.Deserialize<ShoperResponse>(content, ProductServiceHelpers.Default);

                    if (shopResponse?.List != null)
                    {
                        return shopResponse.List
                            .Where(p => p.Translations?.Pl_PL?.Name?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false)
                            .Select(p => new ProductDto
                        {
                            ProductId = p.ProductId,
                            Name = p.Translations!.Pl_PL!.Name,
                            ShortDescription = p.Translations?.Pl_PL?.ShortDescription
                        });
                    }
                }
                else
                {
                    _logger.LogError("Error retrieving products from Shoper API. StatusCode: {StatusCode}", response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while calling Shoper API.");
            }
            return Enumerable.Empty<ProductDto>();
        }
    }
}
