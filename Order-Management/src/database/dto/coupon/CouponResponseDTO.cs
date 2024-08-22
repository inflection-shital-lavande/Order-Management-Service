
using Microsoft.AspNetCore.Mvc.RazorPages;
using order_management.domain_types.enums;
using System.ComponentModel.DataAnnotations;


namespace order_management.database.dto;

public class CouponResponseModel
{

    public Guid Id { get; set; }

    [StringLength(64)]
    [Display(Description = "Name of the coupon")]

    public string? Name { get; set; }

    [StringLength(1024)]
    [Display(Description = "Description of coupon")]

    public string? Description { get; set; }

    [StringLength(64)]
    [Display(Description = "Code of coupon")]


    public string? CouponCode { get; set; }

    [StringLength(64)]
    [Display(Description = "Type of coupon")]

    public string? CouponType { get; set; }
    [Display(Description = "Coupon discount")]

    public float? Discount { get; set; } = 0.00f;
    [Display(Description = "Type of discount")]

    public DiscountTypes DiscountType { get; set; } = DiscountTypes.FLAT;
    [Display(Description = "Percentage of discount")]

    public float? DiscountPercentage { get; set; } = 0.00f;
    [Display(Description = "Max amount of discount")]

    public float? DiscountMaxAmount { get; set; } = 0.00f;
    [Display(Description = "Start date of coupon discount")]

    public DateTime? StartDate { get; set; }
    [Display(Description = "End date of coupon discount")]

    public DateTime? EndDate { get; set; }
    [Display(Description = "Max usage of coupon")]

    public int? MaxUsage { get; set; } = 10000;
    [Display(Description = "Max usage of coupon per user")]

    public int? MaxUsagePerUser { get; set; } = 1;
    [Display(Description = "Max usage of coupon per order")]

    public int? MaxUsagePerOrder { get; set; } = 1;
    [Display(Description = "Minimum order amount to use this coupon")]

    public float? MinOrderAmount { get; set; } = 0.00f;
    [Display(Description = "Coupon is active or not")]

    public bool? IsActive { get; set; } = true;
    [Display(Description = "Coupon is deleted or not")]

    public bool? IsDeleted { get; set; } = false;

    [StringLength(36)]
    [Display(Description = "Id of coupon creator")]

    public Guid? CreatedBy { get; set; }
    [Display(Description = "Created at")]

    public DateTime? CreatedAt { get; set; }
    [Display(Description = "Updated at")]
    public DateTime? UpdatedAt { get; set; }


}



    
   
    
   