using System.Text.Json;

namespace ShoperBackend.Services
{
    internal static class ProductServiceHelpers
    {
        public static readonly JsonSerializerOptions Default = new()
        {
            PropertyNameCaseInsensitive = true,
        };
    }
}