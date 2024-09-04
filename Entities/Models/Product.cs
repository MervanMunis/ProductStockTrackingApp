using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Product : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UUID { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "The name cannot be longer than 200 characters.")]
        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime? DeletionTime { get; set; }
    }
}
