using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ShoperBackend.Models;
using System.Net;
using System.Net.Http.Json;

namespace ShoperBackend.Tests.Controllers;

public class ProductsControllerIntegrationTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Search_WithValidQuery_ReturnsOkResponse()
    {
        string query = "Spódnica Lyon";
        var requestUrl = $"/api/products/search?query={Uri.EscapeDataString(query)}";

        var response = await _client.GetAsync(requestUrl);

        response.EnsureSuccessStatusCode();
        var products = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
        Assert.NotNull(products);
    }

    [Fact]
    public async Task Search_WithShortQuery_ReturnsBadRequestOrEmpty()
    {
        string query = "ab";
        var requestUrl = $"/api/products/search?query={Uri.EscapeDataString(query)}";

        var response = await _client.GetAsync(requestUrl);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
