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
    [ForeignKey("OrderId")]
    public Guid? OrderId { get; set; }

    [MaxLength(36)]
    [ForeignKey("PaymentTransactionId")]

    public Guid? PaymentTransactionId { get; set; }

    [MaxLength(36)]
    [ForeignKey("RefundTransactionId")]

    public Guid? RefundTransactionId { get; set; }

    

    [Required]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    
    public Order Order { get; set; }

    public PaymentTransaction PaymentTransaction { get; set; }

    public PaymentTransaction RefundTransaction { get; set; }




}

