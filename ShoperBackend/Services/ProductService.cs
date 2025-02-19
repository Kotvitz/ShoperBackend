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
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var shopResponse = JsonSerializer.Deserialize<ShoperResponse>(content, ProductServiceHelpers.Default);

                if (shopResponse?.List == null)
                    return [];

                return shopResponse.List
                    .Where(p => p.Translations?.Pl_PL?.Name?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false)
                    .Select(p => new ProductDto
                    {
                        ProductId = p.ProductId,
                        Name = p.Translations!.Pl_PL!.Name,
                        ShortDescription = p.Translations?.Pl_PL?.ShortDescription,
                        Description = p.Translations?.Pl_PL?.Description
                    });
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "HTTP request error: {RequestUri}", requestUri);
                throw new ApplicationException("Error fetching data from Shoper API", httpEx);
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error parsing JSON from Shoper API response.");
                throw new ApplicationException("Invalid response format from Shoper API", jsonEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Shoper API: {RequestUri}", requestUri);
                throw new ApplicationException("Failed to fetch products from the API", ex);
            }
        }
    }
}
