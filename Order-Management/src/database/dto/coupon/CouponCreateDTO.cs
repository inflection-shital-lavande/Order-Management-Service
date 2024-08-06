
using Order_Management.domain_types.enums;
using System.ComponentModel.DataAnnotations;


namespace Order_Management.database.dto
{
    public class CouponCreateDTO
    {
        
        [StringLength(64)]
        [Required]
        public string? Name { get; set; }

        [StringLength(1024)]
        public string? Description { get; set; }

        [StringLength(64)]

        public string? CouponCode { get; set; }

        [StringLength(64)]
        public string? CouponType { get; set; }

        public float? Discount { get; set; } = 0.00f;

        public DiscountTypes DiscountType { get; set; } = DiscountTypes.FLAT;

        public float? DiscountPercentage { get; set; } = 0.00f;

        public float? DiscountMaxAmount { get; set; } = 0.00f;

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
}

