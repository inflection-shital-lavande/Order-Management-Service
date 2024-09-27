using order_management.domain_types.enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace order_management.src.database.dto.orderHistory;

public class OrderHistoryUpdateModel
{

   
    [MaxLength(36)]
    public Guid? OrderId { get; set; }

    public OrderStatusTypes PreviousStatus { get; set; } = OrderStatusTypes.DRAFT;

    public OrderStatusTypes Status { get; set; } = OrderStatusTypes.DRAFT;

    [MaxLength(36)]
    public Guid? UpdatedByUserId { get; set; }

    public DateTime? Timestamp { get; set; }
}

