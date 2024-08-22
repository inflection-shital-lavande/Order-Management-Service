using order_management.domain_types.enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace order_management.src.database.dto
{
    public class OrderResponseModel
    {
        [Required(ErrorMessage = "Order Id is required.")]
        [Description("Id of the order")]
        public Guid Id { get; set; }

        [StringLength(64, MinimumLength = 4, ErrorMessage = "Display code must be between 4 and 64 characters.")]
        [Description("Display code for the order")]
        public string? DisplayCode { get; set; }

        [Description("Invoice number for the order")]
        public string? InvoiceNumber { get; set; }

        [Description("Cart Id for the order")]
        public Guid? AssociatedCartId { get; set; }

        [Range(0, 100, ErrorMessage = "Total items count must be between 0 and 100.")]
        [Description("Total items in the order")]
        public int? TotalItemsCount { get; set; } = 0;

        [Range(0.0, double.MaxValue, ErrorMessage = "Order discount must be a non-negative value.")]
        [Description("Discount applied to the order")]
        public float? OrderDiscount { get; set; } = 0.0f;

        [Description("Is tip applicable for the order")]
        public bool? TipApplicable { get; set; } = false;

        [Range(0.0, double.MaxValue, ErrorMessage = "Tip amount must be a non-negative value.")]
        [Description("Tip amount for the order")]
        public float? TipAmount { get; set; } = 0.0f;

        [Range(0.0, double.MaxValue, ErrorMessage = "Total tax must be a non-negative value.")]
        [Description("Total tax for the order")]
        public float? TotalTax { get; set; } = 0.0f;

        [Range(0.0, double.MaxValue, ErrorMessage = "Total discount must be a non-negative value.")]
        [Description("Total discount for the order")]
        public float? TotalDiscount { get; set; } = 0.0f;

        [Range(0.0, double.MaxValue, ErrorMessage = "Total amount must be a non-negative value.")]
        [Description("Total amount for the order")]
        public float? TotalAmount { get; set; } = 0.0f;

        [StringLength(1024, MinimumLength = 5, ErrorMessage = "Notes must be between 5 and 1024 characters.")]
        [Description("Notes added for the order for the delivery")]
        public string? Notes { get; set; }

        [Description("Coupons applied to the order")]
        public List<Dictionary<string, object>> Coupons { get; set; }

        [Required(ErrorMessage = "Order status is required.")]
        [Description("Order status")]
        public OrderStatusTypes OrderStatus { get; set; } = OrderStatusTypes.DRAFT;

        [StringLength(64, MinimumLength = 2, ErrorMessage = "Order type must be between 2 and 64 characters.")]
        [Description("Order type")]
        public string? OrderType { get; set; }

        [Description("Order creation date")]
        public DateTime? CreatedAt { get; set; }

        [Description("Order last updated date")]
        public DateTime? UpdatedAt { get; set; }
    }
}
