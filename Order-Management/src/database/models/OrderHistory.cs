using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Order_Management.domain_types.enums;

namespace Order_Management.database.models
{
    public class OrderHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(36)]
        public Guid? OrderId { get; set; }

        [Required]
        public OrderStatusTypes PreviousStatus { get; set; }

        [Required]
        public OrderStatusTypes Status { get; set; }

        [MaxLength(36)]
        public Guid? UpdatedByUserId { get; set; }

        [Required]
        public DateTime? Timestamp { get; set; } = DateTime.UtcNow;

        public ICollection<Order> Orders { get; set; }


    }

}
