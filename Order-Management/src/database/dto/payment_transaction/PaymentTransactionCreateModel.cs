using order_management.domain_types.enums;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.payment_transaction
{
    public class PaymentTransactionCreateModel
    {
        [Required]
        public string DisplayCode { get; set; }

        [Required]
        public string InvoiceNumber { get; set; }

        public string BankTransactionId { get; set; }
        public string PaymentGatewayTransactionId { get; set; }
        public PaymentStatusTypes PaymentStatus { get; set; } = PaymentStatusTypes.UNKNOWN;
        public string PaymentMode { get; set; }
        public float PaymentAmount { get; set; } = 0.0f;
        public string PaymentCurrency { get; set; }
        public DateTime InitiatedDate { get; set; } = DateTime.Now;
        public DateTime CompletedDate { get; set; }
        public string PaymentResponse { get; set; }
        public string PaymentResponseCode { get; set; }
        [Required]
        public string InitiatedBy { get; set; }
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        public Guid OrderId { get; set; }
        public bool IsRefund { get; set; } = false;
    }
}
