
using Microsoft.AspNetCore.Mvc.RazorPages;
using order_management.domain_types.enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace order_management.database.dto;

public class CouponUpdateModel
{

    [StringLength(64, MinimumLength = 2, ErrorMessage = "Coupon name must be between 2 and 64 characters.")]

    public string? Name { get; set; }

    [StringLength(1024, MinimumLength = 2, ErrorMessage = "Description must be between 2 and 1024 characters.")]
    public string? Description { get; set; }

    [StringLength(64, MinimumLength = 2, ErrorMessage = "Coupon code must be between 2 and 64 characters.")]

    public string? CouponCode { get; set; }

    [StringLength(64, MinimumLength = 2, ErrorMessage = "Coupon type must be between 2 and 64 characters.")]
    public string? CouponType { get; set; }
    [Range(0.0, double.MaxValue, ErrorMessage = "Discount must be a non-negative value.")]

    public float? Discount { get; set; } = 0.00f;

    public DiscountTypes DiscountType { get; set; } = DiscountTypes.FLAT;
    [Range(0.0, 100.0, ErrorMessage = "Discount percentage must be between 0 and 100.")]

    public float? DiscountPercentage { get; set; } = 0.00f;
    [Range(0.0, double.MaxValue, ErrorMessage = "Discount max amount must be a non-negative value.")]

    public float? DiscountMaxAmount { get; set; } = 0.00f;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    [Range(0, 10000, ErrorMessage = "Max usage must be between 0 and 10000.")]

    public int? MaxUsage { get; set; } = 10000;
    [Range(0, 10, ErrorMessage = "Max usage per user must be between 0 and 10.")]

    public int? MaxUsagePerUser { get; set; } = 1;
    [Range(0, 5, ErrorMessage = "Max usage per order must be between 0 and 5.")]

    public int? MaxUsagePerOrder { get; set; } = 1;
    [Range(0.0, double.MaxValue, ErrorMessage = "Minimum order amount must be a non-negative value.")]

    public float? MinOrderAmount { get; set; } = 0.00f;

    public bool? IsActive { get; set; } = true;

    public bool? IsDeleted { get; set; } = false;

   
}




