using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.app.database.models
{
    public class OrderCoupon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Code { get; set; }

        [Required]
        public Guid? CouponId { get; set; }

        [Required]
        public Guid? OrderId { get; set; }

        [Required]
        public double DiscountValue { get; set; } = 0.0;

        [Required]
        public double DiscountPercentage { get; set; } = 0.0;

        [Required]
        public double DiscountMaxAmount { get; set; } = 0.0;

        [Required]
        public bool Applied { get; set; } = true;

        public DateTime? AppliedAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        

        public OrderCoupon(Guid id, string code, Guid orderId,
                           double discountValue = 0.0, double discountPercentage = 0.0,
                           double discountMaxAmount = 0.0, DateTime? expiryDate = null,
                           bool isActive = true)
        {
            Id = id;
            Code = code;
            OrderId = orderId;
            DiscountValue = discountValue;
            DiscountPercentage = discountPercentage;
            DiscountMaxAmount = discountMaxAmount;
            // ExpiryDate and IsActive can be handled as needed
        }


    }
}
