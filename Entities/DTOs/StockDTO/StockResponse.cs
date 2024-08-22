namespace Entities.DTOs.StockDTO
{
    public class StockResponse
    {
        public Guid UUID { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
