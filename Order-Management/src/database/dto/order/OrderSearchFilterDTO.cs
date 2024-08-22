using order_management.domain_types.enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace order_management.src.database.dto;

public class OrderSearchFilterModel
{
    [Description("Search by the Id of the customer")]
    public Guid? CustomerId { get; set; }

    [Description("Search by the associated cart Id")]
    public Guid? AssociatedCartId { get; set; }

    [Description("Search by the applied coupon Id")]
    public Guid? CouponId { get; set; }

    [Range(0, 100, ErrorMessage = "Total items count must be between 0 and 100.")]
    [Description("Search orders with total items greater than given value")]
    public int? TotalItemsCountGreaterThan { get; set; }

    [Range(1, 100, ErrorMessage = "Total items count must be between 1 and 100.")]
    [Description("Search orders with total items less than given value")]
    public int? TotalItemsCountLessThan { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Order discount must be a non-negative value.")]
    [Description("Search orders with discount applied to the order greater than this value")]
    public float? OrderDiscountGreaterThan { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Order discount must be a non-negative value.")]
    [Description("Search orders with discount applied to the order less than this value")]
    public float? OrderDiscountLessThan { get; set; }

    [Description("Search orders with tip applicable or not")]
    public bool? TipApplicable { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Total amount must be a non-negative value.")]
    [Description("Search orders with total amount greater than this value")]
    public float? TotalAmountGreaterThan { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Total amount must be a non-negative value.")]
    [Description("Search orders with total amount less than this value")]
    public float? TotalAmountLessThan { get; set; }

    [Description("Search orders with the given order line item's product Id")]
    public Guid? OrderLineItemProductId { get; set; }

    [Description("Search orders with the given order status")]
    public OrderStatusTypes? OrderStatus { get; set; }

    [StringLength(64, MinimumLength = 2, ErrorMessage = "Order type must be between 2 and 64 characters.")]
    [Description("Search orders with the given order type")]
    public string OrderType { get; set; }

    [Description("Search orders created before the given date")]
    public DateTime? CreatedBefore { get; set; }

    [Description("Search orders created after the given date")]
    public DateTime? CreatedAfter { get; set; }

    [Range(0, 12, ErrorMessage = "Past months must be between 0 and 12.")]
    [Description("Search orders created in the past given number of months")]
    public int? PastMonths { get; set; }
}
