using Order_Management.app.domain_types.enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.app.domain_types.dto.couponDTO
{
    public class couponSearchFilterDTO
    {
        [StringLength(64)]
        public string? Name { get; set; }

        
        [StringLength(64)]

        public string? CouponCode { get; set; }
        
        public DateTime? StartDate { get; set; }

        public float? Discount { get; set; } = 0.00f;

        public DiscountTypes DiscountType { get; set; } = DiscountTypes.FLAT;

        public float? DiscountPercentage { get; set; } = 0.00f;
     
        public float? MinOrderAmount { get; set; } = 0.00f;
        public bool? IsActive { get; set; } = true;

    }
}




