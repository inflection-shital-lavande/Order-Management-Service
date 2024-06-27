using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.app.database.models
{
    public class OrderPayment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(36)]
        public string OrderId { get; set; }

        [MaxLength(36)]
        public string PaymentTransactionId { get; set; }

        [MaxLength(36)]
        public string RefundTransactionId { get; set; }

        // Navigation properties
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("PaymentTransactionId")]
        public PaymentTransaction PaymentTransaction { get; set; }

        [ForeignKey("RefundTransactionId")]
        public PaymentTransaction RefundTransaction { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        

        public OrderPayment(Guid id, string orderId, string paymentTransactionId = null, string refundTransactionId = null)
        {
            Id = id;
            OrderId = orderId;
            PaymentTransactionId = paymentTransactionId;
            RefundTransactionId = refundTransactionId;
        }
    }
    }
