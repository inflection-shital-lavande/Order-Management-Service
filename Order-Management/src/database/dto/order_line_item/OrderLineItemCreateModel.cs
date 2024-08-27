using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.order_line_item
{
    public class OrderLineItemCreateModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
        [MaxLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string? Name { get; set; }

        [MaxLength(50, ErrorMessage = "CatalogId cannot exceed 50 characters.")]
        public Guid? CatalogId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int? Quantity { get; set; }

        [Required(ErrorMessage = "UnitPrice is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "UnitPrice must be greater than zero.")]
        public double? UnitPrice { get; set; } = 0.0;

        [Range(0.0, double.MaxValue, ErrorMessage = "Discount cannot be negative.")]
        public double? Discount { get; set; } = 0.0;

        [MaxLength(50, ErrorMessage = "DiscountSchemeId cannot exceed 50 characters.")]
        public Guid? DiscountSchemeId { get; set; }

        [Required(ErrorMessage = "Tax is required.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Tax cannot be negative.")]
        public double? Tax { get; set; }

        [Required(ErrorMessage = "ItemSubTotal is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "ItemSubTotal must be greater than zero.")]
        public double? ItemSubTotal { get; set; } = 0.0;

        [Required(ErrorMessage = "OrderId is required.")]
        public Guid? OrderId { get; set; }

        [Required(ErrorMessage = "CartId is required.")]
        public Guid? CartId { get; set; }
    }
}
