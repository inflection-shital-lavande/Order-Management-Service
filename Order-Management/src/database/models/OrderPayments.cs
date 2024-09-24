using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace order_management.database.models;

public class OrderPayment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(36)]
    public Guid? OrderId { get; set; }

    [MaxLength(36)]
    public Guid? PaymentTransactionId { get; set; }

    [MaxLength(36)]
    public Guid? RefundTransactionId { get; set; }

    // Navigation properties
    [ForeignKey("OrderId")]
    public Order Order { get; set; }
   // public virtual Order order { get; set; }

    [ForeignKey("PaymentTransactionId")]
    public PaymentTransaction PaymentTransaction { get; set; }

    [ForeignKey("RefundTransactionId")]
    public PaymentTransaction RefundTransaction { get; set; }

    [Required]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }




}

