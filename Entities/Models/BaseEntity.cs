using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class BaseEntity
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationTime { get; set; }

        public bool IsDeleted { get; set; }
    }
}
