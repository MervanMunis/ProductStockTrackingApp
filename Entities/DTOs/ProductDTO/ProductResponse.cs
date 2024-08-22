namespace Entities.DTOs.ProductDTO
{
    public class ProductResponse
    {
        public Guid UUID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
