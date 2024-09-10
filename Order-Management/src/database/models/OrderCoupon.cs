using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace order_management.database.models;

public class OrderCoupon
{
    /* [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     public Guid Id { get; set; }

     [Required]
     [MaxLength(64)]
     public string? Code { get; set; }

     [Required]
     public Guid? CouponId { get; set; }

     [Required]
     public Guid? OrderId { get; set; }

     [Required]
     public double? DiscountValue { get; set; } = 0.0;

     [Required]
     public double? DiscountPercentage { get; set; } = 0.0;

     [Required]
     public double? DiscountMaxAmount { get; set; } = 0.0;

     [Required]
     public bool? Applied { get; set; } = true;

     public DateTime? AppliedAt { get; set; }

     [Required]
     public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

     public DateTime? UpdatedAt { get; set; }

     public virtual Order Orders { get; set; }*/

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }// = Guid.NewGuid();

    [MaxLength(64)]
    public string? Code { get; set; }

    [Required]
    public Guid CouponId { get; set; }

    [ForeignKey("CouponId")]
    public Coupon? Coupon { get; set; }

    [Required]
    public Guid OrderId { get; set; }

    [ForeignKey("OrderId")]
    public Order? Order { get; set; }

    public double DiscountValue { get; set; } = 0.0;

    public double DiscountPercentage { get; set; } = 0.0;

    public double DiscountMaxAmount { get; set; } = 0.0;

    public bool Applied { get; set; } = true;

    public DateTime? AppliedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? UpdatedAt { get; set; }




}
