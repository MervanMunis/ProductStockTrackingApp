using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Stock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UUID { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationTime { get; set; }
        public bool IsDeleted { get; set; }

        // Navigation property
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

    }
}
