using System;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.cart
{
    public class CartSearchFilter
    {
        public Guid? CustomerId { get; set; }

        public Guid? ProductId { get; set; }

        [Range(0, 100, ErrorMessage = "Total items count must be between 0 and 100.")]
        public int? TotalItemsCountGreaterThan { get; set; }

        [Range(1, 100, ErrorMessage = "Total items count must be between 1 and 100.")]
        public int? TotalItemsCountLessThan { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Total amount must be a non-negative value.")]
        public float? TotalAmountGreaterThan { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Total amount must be a non-negative value.")]
        public float? TotalAmountLessThan { get; set; }

        public DateTime? CreatedBefore { get; set; }

        public DateTime? CreatedAfter { get; set; }
    }
}


