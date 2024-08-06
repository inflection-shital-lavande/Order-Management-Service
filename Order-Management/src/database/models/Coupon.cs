using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using Order_Management.domain_types.enums;


namespace Order_Management.database.models
{
    public class Coupon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]

        [StringLength(64)]
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

       // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedAt { get; set; }

      //  [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedAt { get; set; }

        public ICollection<OrderCoupon> OrderCoupons { get; set; }
       
    }

    }
