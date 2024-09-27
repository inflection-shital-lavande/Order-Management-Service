using order_management.domain_types.enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace order_management.src.database.dto.orderHistory;

public class OrderHistoryCreateModel
{

    [Required]
    public Guid? OrderId { get; set; }

    [Required]
    public OrderStatusTypes PreviousStatus { get; set; } = OrderStatusTypes.DRAFT;

    [Required]
    public OrderStatusTypes Status { get; set; } = OrderStatusTypes.DRAFT;

    [Required]
    public Guid? UpdatedByUserId { get; set; }

    [Required]
    public DateTime? Timestamp { get; set; } 
}


