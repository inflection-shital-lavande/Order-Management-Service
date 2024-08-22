using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace order_management.database.models;

public class OrderCoupon
{
    [Key]
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

    public virtual Order Order { get; set; }




}
