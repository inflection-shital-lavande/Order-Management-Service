using order_management.domain_types.enums;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.payment_transaction
{
    public class PaymentTransactionCreateModel
    {
        [Required]
        public string? DisplayCode { get; set; }

        [Required]
        public string? InvoiceNumber { get; set; }
        [Required]

        public Guid? BankTransactionId { get; set; }
        [Required]
        public Guid? PaymentGatewayTransactionId { get; set; }
        [Required]
        public PaymentStatusTypes PaymentStatus { get; set; } = PaymentStatusTypes.UNKNOWN;
        public string? PaymentMode { get; set; }
        [Required]
        public double? PaymentAmount { get; set; } = 0.0;
        [Required]
        public decimal? PaymentCurrency { get; set; }
        [Required]
        public DateTime? InitiatedDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime? CompletedDate { get; set; }
        [Required]
        public string? PaymentResponse { get; set; }
        [Required]
        public string? PaymentResponseCode { get; set; }
        [Required]
        public string? InitiatedBy { get; set; }
        [Required]
        public Guid? CustomerId { get; set; }
        [Required]
        public Guid? OrderId { get; set; }
        public bool? IsRefund { get; set; } = false;
    }
}
