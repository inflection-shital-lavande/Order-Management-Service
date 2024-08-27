using System;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.cart
{
    public class CartCreateModel
    {
        [Required(ErrorMessage = "Customer ID is required.")]
        public Guid? CustomerId { get; set; }

        public Guid? AssociatedOrderId { get; set; }

        public DateTime? CartToOrderTimestamp { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Total items count must be greater than 0.")]
        public int? TotalItemsCount { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than 0.")]
        public float? TotalAmount { get; set; }
    }
}

