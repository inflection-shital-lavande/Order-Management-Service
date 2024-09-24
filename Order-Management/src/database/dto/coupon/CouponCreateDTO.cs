
using Microsoft.AspNetCore.Mvc.RazorPages;
using order_management.domain_types.enums;
using System;
using System.ComponentModel.DataAnnotations;


namespace order_management.database.dto;

public class CouponCreateModel
{
    /*  [Required(ErrorMessage = "Name is required.")]
      [MinLength(2, ErrorMessage = "Name must be at least 2 characters long.")]
      [MaxLength(64, ErrorMessage = "Name cannot exceed 64 characters.")]
      public string Name { get; set; }

      [Required(ErrorMessage = "Description is required.")]
      [MinLength(2, ErrorMessage = "Description must be at least 2 characters long.")]
      [MaxLength(1024, ErrorMessage = "Description cannot exceed 1024 characters.")]
      public string Description { get; set; }

      [Required(ErrorMessage = "CouponCode is required.")]
      [MinLength(2, ErrorMessage = "CouponCode must be at least 2 characters long.")]
      [MaxLength(64, ErrorMessage = "CouponCode cannot exceed 64 characters.")]
      public string CouponCode { get; set; }

      [Required(ErrorMessage = "CouponType is required.")]
      [MinLength(2, ErrorMessage = "CouponType must be at least 2 characters long.")]
      [MaxLength(64, ErrorMessage = "CouponType cannot exceed 64 characters.")]
      public string CouponType { get; set; }

      [Range(0.0, double.MaxValue, ErrorMessage = "Discount must be a positive number.")]
      public float? Discount { get; set; } = 0.0f;

      [Required(ErrorMessage = "DiscountType is required.")]
      public DiscountTypes DiscountType { get; set; } = DiscountTypes.FLAT;

      [Range(0.0, 100.0, ErrorMessage = "DiscountPercentage must be between 0 and 100.")]
      public float? DiscountPercentage { get; set; } = 0.0f;

      [Range(0.0, double.MaxValue, ErrorMessage = "DiscountMaxAmount must be a positive number.")]
      public float? DiscountMaxAmount { get; set; } = 0.0f;

      [Required(ErrorMessage = "StartDate is required.")]
      public DateTime? StartDate { get; set; } = DateTime.Now;

      public DateTime? EndDate { get; set; }

      [Range(0, 10000, ErrorMessage = "MaxUsage must be between 0 and 10,000.")]
      public int? MaxUsage { get; set; } = 1000;

      [Range(0, 10, ErrorMessage = "MaxUsagePerUser must be between 0 and 10.")]
      public int? MaxUsagePerUser { get; set; } = 1;

      [Range(0, 5, ErrorMessage = "MaxUsagePerOrder must be between 0 and 5.")]
      public int? MaxUsagePerOrder { get; set; } = 1;

      [Range(0.0, double.MaxValue, ErrorMessage = "MinOrderAmount must be a positive number.")]
      public float? MinOrderAmount { get; set; } = 100.0f;

      public bool? IsActive { get; set; } = true;
      public bool? IsDeleted { get; set; } = false;

      public Guid? CreatedBy { get; set; }*/

      [Required(ErrorMessage = "Name is required.")]
      public string? Name { get; set; }

      [Required(ErrorMessage = "Description is required.")]
      public string? Description { get; set; }

      [Required(ErrorMessage = "Coupon code is required.")]
      public string? CouponCode { get; set; }

      [Required(ErrorMessage = "Coupon type is required.")]
      public string? CouponType { get; set; }
     
      [Range(0.0, double.MaxValue, ErrorMessage = "Discount must be a positive number.")]
      public float? Discount { get; set; } = 0.00f;

      [Required(ErrorMessage = "Discount type is required.")]
      public DiscountTypes DiscountType { get; set; } = DiscountTypes.FLAT;

    
      public float? DiscountPercentage { get; set; } = 0.00f;

      [Range(0.0, double.MaxValue, ErrorMessage = "DiscountMaxAmount must be a positive number.")]
      public float? DiscountMaxAmount { get; set; } = 0.00f;

      [Required(ErrorMessage = "Start date is required.")]

      public DateTime? StartDate { get; set; }

      public DateTime? EndDate { get; set; }
      public int? MaxUsage { get; set; } = 10000;
     
      public int? MaxUsagePerUser { get; set; } = 1;
   
      public int? MaxUsagePerOrder { get; set; } = 1;

      public float? MinOrderAmount { get; set; } = 0.00f;

      public bool? IsActive { get; set; } = true;

      public bool? IsDeleted { get; set; } = false;
      [StringLength(36)]
      public Guid? CreatedBy { get; set; }

}

