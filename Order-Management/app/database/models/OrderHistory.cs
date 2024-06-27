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
        public string OrderId { get; set; }

        [Required]
        public OrderStatusTypes PreviousStatus { get; set; }

        [Required]
        public OrderStatusTypes Status { get; set; }

        [MaxLength(36)]
        public string UpdatedByUserId { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;



        public OrderHistory(Guid id, string orderId, OrderStatusTypes status, OrderStatusTypes previousStatus, string updatedByUserId = null)
        {
            Id = id;
            OrderId = orderId;
            Status = status;
            PreviousStatus = previousStatus;
            UpdatedByUserId = updatedByUserId;
        }

      }

    }
