using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.order_line_item
{
    public class OrderLineItemResponseModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; } = string.Empty;

        public Guid? CatalogId { get; set; }

        [Required]
        public int? Quantity { get; set; }

        [Required]
        public double? UnitPrice { get; set; } = 0.0;

        public double? Discount { get; set; } = 0.0;

        public Guid? DiscountSchemeId { get; set; }

        [Required]
        public double? Tax { get; set; } = 0.0;

        [Required]
        public double? ItemSubTotal { get; set; } = 0.0;
       

        [Required]
        public Guid? OrderId { get; set; }
        public Dictionary<string, object>? Orders { get; set; }

        [Required]
        public Guid? CartId { get; set; }
        public Dictionary<string, object>? Carts { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
