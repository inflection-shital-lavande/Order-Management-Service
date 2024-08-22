using order_management.domain_types.enums;

namespace Order_Management.src.database.dto.payment_transaction
{
    public class PaymentTransactionResponseModel
    {
        public Guid Id { get; set; }
        public string DisplayCode { get; set; }
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
        public string InitiatedBy { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OrderId { get; set; }
        public bool IsRefund { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
