using order_management.domain_types.enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace order_management.src.database.dto.orderHistory;

public class OrderHistoryCreateModel
{
    
   
    [MaxLength(36)]
    public Guid? OrderId { get; set; }

    public OrderStatusTypes PreviousStatus { get; set; } = OrderStatusTypes.DRAFT;

    [Required]
    public OrderStatusTypes Status { get; set; } = OrderStatusTypes.DRAFT;

    [MaxLength(36)]
    public Guid? UpdatedByUserId { get; set; }

    [Required]
    public DateTime? Timestamp { get; set; } 
}


