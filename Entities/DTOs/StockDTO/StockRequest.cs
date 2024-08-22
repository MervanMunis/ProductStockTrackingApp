using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs.StockDTO
{
    public class StockRequest
    {
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
