
using order_management.domain_types.enums;
using System.ComponentModel.DataAnnotations;

namespace order_management.database.dto;

public class CouponSearchFilterModel
{
    [Display(Description = "Search coupon by name")]
    public string? Name { get; set; }

    [Display(Description = "Search coupon by coupon code")]
    public string? CouponCode { get; set; }

    [Display(Description = "Search coupon by start date")]
    public DateTime? StartDate { get; set; }

    [Display(Description = "Search coupon by discount")]
    public float? Discount { get; set; } = 0.00f;

    [Display(Description = "Search coupon by discount type")]
    public DiscountTypes DiscountType { get; set; } = DiscountTypes.FLAT;

    [Display(Description = "Search coupon by discount percentage")]
    public float? DiscountPercentage { get; set; } = 0.00f;

    [Display(Description = "Search coupon by minimum order amount.")]
    public float? MinOrderAmount { get; set; } = 0.00f;
    [Display(Description = "Search coupon by its state")]
    public bool? IsActive { get; set; } = true;

}

