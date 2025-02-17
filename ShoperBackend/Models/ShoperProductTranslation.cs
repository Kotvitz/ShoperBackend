using System.Text.Json.Serialization;

namespace ShoperBackend.Models
{
    public class ShoperProductTranslation
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("short_description")]
        public string? ShortDescription { get; set; }
    }
}