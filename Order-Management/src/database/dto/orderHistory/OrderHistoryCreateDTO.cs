using Order_Management.domain_types.enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Order_Management.src.database.dto.orderHistory
{
    public class OrderHistoryCreateDTO
    {
        
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


