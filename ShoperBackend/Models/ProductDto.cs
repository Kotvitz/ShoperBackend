namespace ShoperBackend.Models
{
    public class ProductDto
    {
        public required string ProductId { get; set; }
        public required string Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
    }
}
