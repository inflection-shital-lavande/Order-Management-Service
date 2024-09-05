using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using order_management.domain_types.enums;
using System.Text.Json.Serialization;

namespace order_management.database.models;

public class PaymentTransaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [MaxLength(36)]
    
    public string? DisplayCode { get; set; }

    [MaxLength(64)]       
    public string? InvoiceNumber { get; set; }

    [MaxLength(36)]
    public Guid? BankTransactionId { get; set; }

    [MaxLength(36)]
    public Guid? PaymentGatewayTransactionId { get; set; }

    [Required]
    public PaymentStatusTypes PaymentStatus { get; set; } = PaymentStatusTypes.UNKNOWN;

    [MaxLength(36)]
    public string? PaymentMode { get; set; }

    [Required]
    public double? PaymentAmount { get; set; } = 0.0;

    public decimal? PaymentCurrency { get; set; }

    public DateTime? InitiatedDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    [MaxLength(1024)]
    public string? PaymentResponse { get; set; }

    [MaxLength(36)]
    public string? PaymentResponseCode { get; set; }

    [MaxLength(36)]
    public string? InitiatedBy { get; set; }

    [ForeignKey("CustomerId")]
    public Guid? CustomerId { get; set; }

    [ForeignKey("OrderId")]
    public Guid? OrderId { get; set; }

    [Required]
    public bool? IsRefund { get; set; } = false;

    [Required]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    //navigation properties 
    [JsonIgnore]
    public virtual Order Order { get; set; }
    [JsonIgnore]

    public virtual Customer Customer { get; set; }
    public ICollection<OrderPayment> OrderPaymentTransection { get; set; }


}

