using System.Text.Json.Serialization;

namespace ShoperBackend.Models
{
    public class ShoperProduct
    {
        [JsonPropertyName("product_id")]
        public required string ProductId { get; set; }

        [JsonPropertyName("translations")]
        public required ShoperTranslations Translations { get; set; }
    }
}