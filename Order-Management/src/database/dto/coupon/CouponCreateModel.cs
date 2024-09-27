
using Microsoft.AspNetCore.Mvc.RazorPages;
using order_management.domain_types.enums;
using System;
using System.ComponentModel.DataAnnotations;


namespace order_management.database.dto;

public class CouponCreateModel
{
    

      [Required]
      public string? Name { get; set; }

      [Required]
      public string? Description { get; set; }

      [Required]
      public string? CouponCode { get; set; }

      [Required]
      public string? CouponType { get; set; }
     
      [Range(0.0, double.MaxValue)]
      public float? Discount { get; set; } = 0.00f;

      [Required]
      public DiscountTypes DiscountType { get; set; } = DiscountTypes.FLAT;

      [Range(0.0, 100.0)]
      public float? DiscountPercentage { get; set; } = 0.00f;
      [Range(0.0, Double.MaxValue)]
      public float? DiscountMaxAmount { get; set; } = 0.00f;

      [Required]

      public DateTime? StartDate { get; set; } = DateTime.Now;

      public DateTime? EndDate { get; set; }
      public int? MaxUsage { get; set; } = 1000;
     
      public int? MaxUsagePerUser { get; set; } = 1;
   
      public int? MaxUsagePerOrder { get; set; } = 1;

      public float? MinOrderAmount { get; set; } = 0.00f;

      public bool? IsActive { get; set; } = true;

      public bool? IsDeleted { get; set; } = false;
      [StringLength(36)]
      public Guid? CreatedBy { get; set; }

}







