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
    public Dictionary<string, object>? Orders { get; set; }

    //[Required]
    public OrderStatusTypes PreviousStatus { get; set; } = OrderStatusTypes.DRAFT;

   // [Required]
    public OrderStatusTypes Status { get; set; } = OrderStatusTypes.DRAFT;

    [MaxLength(36)]
    public Guid? UpdatedByUserId { get; set; }

   // [Required]
    public DateTime? Timestamp { get; set; }

  

}
