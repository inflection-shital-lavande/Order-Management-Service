using System;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.cart
{
    public class CartCreateModel
    {
        [Required(ErrorMessage = "CustomerId is required")]
        public Guid? CustomerId { get; set; }

        public Guid? AssociatedOrderId { get; set; }

        public DateTime? CartToOrderTimestamp { get; set; }
        
        public int? TotalItemsCount { get; set; }
       
        public float? TotalAmount { get; set; }
    }
}

