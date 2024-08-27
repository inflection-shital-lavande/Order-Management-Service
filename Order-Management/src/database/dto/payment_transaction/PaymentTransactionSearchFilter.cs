namespace Order_Management.src.database.dto.payment_transaction
{
    public class PaymentTransactionSearchFilter
    {
        public string? DisplayCode { get; set; }
        public string? InvoiceNumber { get; set; }
        public Guid? BankTransactionId { get; set; }
        public DateTime? InitiatedDate { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? OrderId { get; set; }
        public string? PaymentMode { get; set; }
        public float? PaymentAmount { get; set; }
        public bool? IsRefund { get; set; } = false;
    }
}
