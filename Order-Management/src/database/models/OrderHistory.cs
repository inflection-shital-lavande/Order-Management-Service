using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using order_management.domain_types.enums;

namespace order_management.database.models;

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
    // one to one 
    public virtual Order Order { get; set; }


}
