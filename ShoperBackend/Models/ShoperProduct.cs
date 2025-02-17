namespace ShoperBackend.Models
{
    public class ShoperProduct
    {
        public required string ProductId { get; set; }
        public required ShoperTranslations Translations { get; set; }
    }
}