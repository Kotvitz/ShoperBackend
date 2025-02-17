using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using ShoperBackend.Services;
using ShoperBackend.Tests.Utilities;
using System.Net;

namespace ShoperBackend.Tests.Service
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task SearchProductsAsync_WithShortSearchTerm_ReturnsEmpty()
        {
            var handler = new FakeHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("[]")
            });
            var httpClient = new HttpClient(handler);
            var inMemorySettings = new Dictionary<string, string?>
        {
            { "Shoper:ShopUrl", "https://dummyurl" }
        };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            var loggerMock = new Mock<ILogger<ProductService>>();
            var service = new ProductService(httpClient, configuration, loggerMock.Object);

            var result = await service.SearchProductsAsync("ab");

            Assert.Empty(result);
        }

        [Fact]
        public async Task SearchProductsAsync_WithValidResponse_ReturnsMappedProducts()
        {
            string jsonResponse = @"{
            ""count"": ""1"",
            ""pages"": 1,
            ""page"": 1,
            ""list"": [
                {
                    ""product_id"": ""30"",
                    ""translations"": {
                        ""pl_PL"": {
                            ""name"": ""Sukienka o³ówkowa SunnyDay"",
                            ""short_description"": ""<p>Wiskozowa sukienka z pó³golfem</p>""
                        }
                    }
                }
            ]
        }";
            var handler = new FakeHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse)
            });
            var httpClient = new HttpClient(handler);
            var inMemorySettings = new Dictionary<string, string?>
        {
            { "Shoper:ShopUrl", "https://dummyurl" }
        };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            var loggerMock = new Mock<ILogger<ProductService>>();
            var service = new ProductService(httpClient, configuration, loggerMock.Object);

            var result = await service.SearchProductsAsync("Sukienka");

            var productList = result.ToList();
            Assert.Single(productList);
            Assert.Equal("30", productList[0].ProductId);
            Assert.Equal("Sukienka o³ówkowa SunnyDay", productList[0].Name);
            Assert.Contains("Wiskozowa", productList[0].ShortDescription);
        }
    }
}