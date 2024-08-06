using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto
{
    public class OrderCreateDTO
    {
        public Guid? OrderType { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        public Guid? AssociatedCartId { get; set; }

        public bool? TipApplicable { get; set; } = false;

        [MaxLength(1024)]
        [MinLength(5)]
        public string Notes { get; set; }
    }
}
