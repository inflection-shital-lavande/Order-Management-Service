using Order_Management.app.domain_types.enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.app.database.models
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
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;



      
      }

    }
