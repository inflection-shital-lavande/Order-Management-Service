using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.order_line_item
{
    public class OrderLineItemResponseModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(50)]
        public string? CatalogId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public decimal? Discount { get; set; }

        [StringLength(50)]
        public string? DiscountSchemeId { get; set; }

        [Required]
        public decimal Tax { get; set; }

        [Required]
        public decimal ItemSubTotal { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderId { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string CartId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
