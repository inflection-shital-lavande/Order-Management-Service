using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Order_Management.app.domain_types.enums;

namespace Order_Management.app.database.models
{
    public class PaymentTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(36)]
        //[Index(IsUnique = true)]
        public string DisplayCode { get; set; }

        [MaxLength(64)]
        //[Index(IsUnique = true)]
        public string InvoiceNumber { get; set; }

        [MaxLength(36)]
        public string BankTransactionId { get; set; }

        [MaxLength(36)]
        public string PaymentGatewayTransactionId { get; set; }

        [Required]
        public PaymentStatusTypes PaymentStatus { get; set; } = PaymentStatusTypes.UNKNOWN;

        [MaxLength(36)]
        public string PaymentMode { get; set; }

        [Required]
        public double PaymentAmount { get; set; } = 0.0;

        [MaxLength(36)]
        public string PaymentCurrency { get; set; }

        public DateTime? InitiatedDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        [MaxLength(1024)]
        public string PaymentResponse { get; set; }

        [MaxLength(36)]
        public string PaymentResponseCode { get; set; }

        [MaxLength(36)]
        public string InitiatedBy { get; set; }

        [ForeignKey("CustomerId")]
        public string CustomerId { get; set; }

        [ForeignKey("OrderId")]
        public string OrderId { get; set; }

        [Required]
        public bool IsRefund { get; set; } = false;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        

        public PaymentTransaction(Guid id, string displayCode, string invoiceNumber, PaymentStatusTypes paymentStatus, double paymentAmount,
                                  string paymentCurrency = null, string paymentMode = null, DateTime? initiatedDate = null, DateTime? completedDate = null,
                                  string paymentResponse = null, string paymentResponseCode = null, string initiatedBy = null, string customerId = null,
                                  string orderId = null, bool isRefund = false)
        {
            Id = id;
            DisplayCode = displayCode;
            InvoiceNumber = invoiceNumber;
            PaymentStatus = paymentStatus;
            PaymentAmount = paymentAmount;
            PaymentCurrency = paymentCurrency;
            PaymentMode = paymentMode;
            InitiatedDate = initiatedDate;
            CompletedDate = completedDate;
            PaymentResponse = paymentResponse;
            PaymentResponseCode = paymentResponseCode;
            InitiatedBy = initiatedBy;
            CustomerId = customerId;
            OrderId = orderId;
            IsRefund = isRefund;
        }

    }
    }
