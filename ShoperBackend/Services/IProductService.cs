using ShoperBackend.Models;

namespace ShoperBackend.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm);
    }
}
