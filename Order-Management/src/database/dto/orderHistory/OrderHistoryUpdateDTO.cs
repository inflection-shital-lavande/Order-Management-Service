using order_management.domain_types.enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace order_management.src.database.dto.orderHistory;

public class OrderHistoryUpdateModel
{

    [Required]
    [MaxLength(36)]
    public Guid? OrderId { get; set; }

    [Required(ErrorMessage = "PreviousStatus is required.")]
    public OrderStatusTypes PreviousStatus { get; set; } = OrderStatusTypes.DRAFT;

    [Required(ErrorMessage = "Status is required.")]
    public OrderStatusTypes Status { get; set; } = OrderStatusTypes.DRAFT;

    [MaxLength(36)]
    public Guid? UpdatedByUserId { get; set; }

    [Required]
    public DateTime? Timestamp { get; set; }
}

