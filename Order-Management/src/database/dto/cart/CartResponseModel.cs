using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Order_Management.src.database.dto.cart
{
    public class CartResponseModel
    {
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public float? Discount { get; set; }
        public float? TotalTax { get; set; }
        public float? TotalDiscount { get; set; }
        public int? TotalItemsCount { get; set; }
        public float? TotalAmount { get; set; }
        public Guid? AssociatedOrderId { get; set; }
        public DateTime? CartToOrderTimestamp { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Dictionary<string, object>? Customers { get; set; }

    }
}


