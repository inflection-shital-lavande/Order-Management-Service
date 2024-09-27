using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using order_management.domain_types.enums;
using System.Text.Json.Serialization;

namespace order_management.database.models;

public class OrderHistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(36)]
    [ForeignKey("OrderId")]

    public Guid? OrderId { get; set; }

    [Required]
    public OrderStatusTypes PreviousStatus { get; set; } =OrderStatusTypes.DRAFT;

    [Required]
    public OrderStatusTypes Status { get; set; } = OrderStatusTypes.DRAFT;

    [MaxLength(36)]
    public Guid? UpdatedByUserId { get; set; }

    [Required]
    public DateTime? Timestamp { get; set; } 
    // one to one 
    [JsonIgnore]
    public virtual Order Orders { get; set; }


}
