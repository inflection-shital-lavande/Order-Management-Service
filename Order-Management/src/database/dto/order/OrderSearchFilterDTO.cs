using Order_Management.domain_types.enums;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto
{
    public class OrderSearchFilterDTO
    {
        public Guid? CustomerId { get; set; }
        public Guid? AssociatedCartId { get; set; }
        public Guid? CouponId { get; set; }

        [Range(0, 100)]
        public int? TotalItemsCountGreaterThan { get; set; }

        [Range(1, 100)]
        public int? TotalItemsCountLessThan { get; set; }

        [Range(0.0, double.MaxValue)]
        public double? OrderDiscountGreaterThan { get; set; }

        [Range(0.0, double.MaxValue)]
        public double? OrderDiscountLessThan { get; set; }

        public bool? TipApplicable { get; set; }

        [Range(0.0, double.MaxValue)]
        public double? TotalAmountGreaterThan { get; set; }

        [Range(0.0, double.MaxValue)]
        public double? TotalAmountLessThan { get; set; }

        public Guid? OrderLineItemProductId { get; set; }

        public OrderStatusTypes? OrderStatus { get; set; }

        [StringLength(64, MinimumLength = 2)]
        public string? OrderType { get; set; }

        public DateTime? CreatedBefore { get; set; }
        public DateTime? CreatedAfter { get; set; }

        [Range(0, 12)]
        public int? PastMonths { get; set; }
    }
}
