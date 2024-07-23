﻿using System.ComponentModel.DataAnnotations.Schema;
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
        
        public Guid? DisplayCode { get; set; }

        [MaxLength(64)]       
        public string InvoiceNumber { get; set; }

        [MaxLength(36)]
        public Guid? BankTransactionId { get; set; }

        [MaxLength(36)]
        public Guid? PaymentGatewayTransactionId { get; set; }

        [Required]
        public PaymentStatusTypes PaymentStatus { get; set; } = PaymentStatusTypes.UNKNOWN;

        [MaxLength(36)]
        public Guid? PaymentMode { get; set; }

        [Required]
        public double PaymentAmount { get; set; } = 0.0;

        [MaxLength(36)]
        public Guid? PaymentCurrency { get; set; }

        public DateTime? InitiatedDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        [MaxLength(1024)]
        public string PaymentResponse { get; set; }

        [MaxLength(36)]
        public Guid? PaymentResponseCode { get; set; }

        [MaxLength(36)]
        public Guid? InitiatedBy { get; set; }

        [ForeignKey("CustomerId")]
        public Guid? CustomerId { get; set; }

        [ForeignKey("OrderId")]
        public Guid? OrderId { get; set; }

        [Required]
        public bool IsRefund { get; set; } = false;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public ICollection<OrderPayment> OrderPayments { get; set; }

    }
    }
