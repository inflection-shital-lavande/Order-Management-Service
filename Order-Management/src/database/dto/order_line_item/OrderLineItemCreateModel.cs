using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.order_line_item
{
    public class OrderLineItemCreateModel
    {
        [Required]
        public string? Name { get; set; }

        public Guid? CatalogId { get; set; }
        [Required]
        public int? Quantity { get; set; }

        [Required]
        public double? UnitPrice { get; set; } = 0.0;

        public double? Discount { get; set; } = 0.0;

        public Guid? DiscountSchemeId { get; set; }

        [Required(ErrorMessage = "Tax is required.")]
        public double? Tax { get; set; }
        [Required]
        public double? ItemSubTotal { get; set; } = 0.0;

        [Required(ErrorMessage = "OrderId is required.")]
        public Guid? OrderId { get; set; }

        [Required(ErrorMessage = "CartId is required.")]
        public Guid? CartId { get; set; }
    }
}
