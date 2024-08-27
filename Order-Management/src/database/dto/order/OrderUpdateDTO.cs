using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace order_management.src.database.dto;

public class OrderUpdateModel
{
    [Description("Id of the order type")]
    public Guid? OrderType { get; set; }

    [Description("Id of the cart")]
    public Guid? AssociatedCartId { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Order discount must be a non-negative value.")]
    [Description("Discount applied to the order")]
    public double? OrderDiscount { get; set; } = 0.0;

    [Description("Tip applicable or not")]
    public bool? TipApplicable { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Tip amount must be a non-negative value.")]
    [Description("Tip amount")]
    public double? TipAmount { get; set; } = 0.0;

    [StringLength(1024, MinimumLength = 5, ErrorMessage = "Notes must be between 5 and 1024 characters.")]
    [Description("Notes for the delivery")]
    public string? Notes { get; set; }
}
