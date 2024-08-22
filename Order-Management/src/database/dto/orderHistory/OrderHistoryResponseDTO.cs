using order_management.domain_types.enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace order_management.src.database.dto.orderHistory;

public class OrderHistoryResponseModel
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
