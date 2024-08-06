using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto
{
    public class OrderResponseDTO
    {
        [Required]
        public Guid Id { get; set; }

        [StringLength(64, MinimumLength = 4)]
        public string? DisplayCode { get; set; }

        [StringLength(64)]
        public string? InvoiceNumber { get; set; }

        public Guid? AssociatedCartId { get; set; }

        [Range(0, 100)]
        public int TotalItemsCount { get; set; } = 0;

        [Range(0.0, double.MaxValue)]
        public double OrderDiscount { get; set; } = 0.0;

        public bool TipApplicable { get; set; } = false;

        [Range(0.0, double.MaxValue)]
        public double TipAmount { get; set; } = 0.0;

        [Range(0.0, double.MaxValue)]
        public double TotalTax { get; set; } = 0.0;

        [Range(0.0, double.MaxValue)]
        public double TotalDiscount { get; set; } = 0.0;

        [Range(0.0, double.MaxValue)]
        public double TotalAmount { get; set; } = 0.0;

        [StringLength(1024, MinimumLength = 5)]
        public string? Notes { get; set; }
    }
}
