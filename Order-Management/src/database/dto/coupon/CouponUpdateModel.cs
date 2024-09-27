
using Microsoft.AspNetCore.Mvc.RazorPages;
using order_management.domain_types.enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace order_management.database.dto;

public class CouponUpdateModel
{


     [StringLength(64)]
     public string? Name { get; set; }
     [StringLength(1024)]
     public string? Description { get; set; }

     [StringLength(64)]
     public string? CouponCode { get; set; }
     [StringLength(64)]
     public string? CouponType { get; set; }
    [Range(0.0, Double.MaxValue)]
    public float? Discount { get; set; } = 0;

     public DiscountTypes? DiscountType { get; set; } = DiscountTypes.FLAT;

     public float? DiscountPercentage { get; set; } = 0.0f;

    [Range(0.0, Double.MaxValue)]
     public float? DiscountMaxAmount { get; set; } = 0.0f;

     public DateTime? StartDate { get; set; } = DateTime.Now;

     public DateTime? EndDate { get; set; }
     [Range(0, 10000)]
     public int? MaxUsage { get; set; } = 1000;
     [Range(0, 10)]
     public int? MaxUsagePerUser { get; set; } = 1;
     [Range(0, 5)]
     public int? MaxUsagePerOrder { get; set; } = 1;

     [Range(0.0, Double.MaxValue)]
     public float? MinOrderAmount { get; set; } = 100.0f;

     public bool? IsActive { get; set; } = true;

     public bool? IsDeleted { get; set; } = false;
}



























/* public float? Discount { get; set; } = 0.00f;
 [Required]

 public DiscountTypes DiscountType { get; set; } = DiscountTypes.FLAT;

 public float? DiscountPercentage { get; set; } = 0.00f;

 public float? DiscountMaxAmount { get; set; } = 0.00f;

 [Required]
 public DateTime? StartDate { get; set; }

 public DateTime? EndDate { get; set; }
 public int? MaxUsage { get; set; } = 10000;

 public int? MaxUsagePerUser { get; set; } = 1;

 public int? MaxUsagePerOrder { get; set; } = 1;

 public float? MinOrderAmount { get; set; } = 0.00f;

 public bool? IsActive { get; set; } = true;

 public bool? IsDeleted { get; set; } = false;*/







