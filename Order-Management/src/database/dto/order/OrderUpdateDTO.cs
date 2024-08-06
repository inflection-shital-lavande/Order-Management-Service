using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto
{
    public class OrderUpdateDTO
    {
        public Guid? OrderType { get; set; }
        public Guid? AssociatedCartId { get; set; }

        [Range(0.0, double.MaxValue)]
        public double? OrderDiscount { get; set; }

        public bool? TipApplicable { get; set; }

        [Range(0.0, double.MaxValue)]
        public double? TipAmount { get; set; }

        [StringLength(1024, MinimumLength = 5)]
        public string? Notes { get; set; }
    }
}
