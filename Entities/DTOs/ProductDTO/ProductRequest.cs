using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs.ProductDTO
{
    public class ProductRequest
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
    }
}
