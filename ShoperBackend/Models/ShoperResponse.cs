using System.Text.Json.Serialization;

namespace ShoperBackend.Models
{
    public class ShoperResponse
    {
        [JsonPropertyName("count")]
        public required string Count { get; set; }

        [JsonPropertyName("pages")]
        public int Pages { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("list")]
        public List<ShoperProduct>? List { get; set; } = [];
    }
}
