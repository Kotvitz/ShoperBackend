using System.Text.Json.Serialization;

namespace ShoperBackend.Models
{
    public class ShoperTranslations
    {
        [JsonPropertyName("pl_PL")]
        public required ShoperProductTranslation Pl_PL { get; set; }
    }
}