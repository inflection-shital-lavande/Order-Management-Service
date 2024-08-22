using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.order_line_item
{
    public class OrderLineItemSearchFilter
    {
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? CatalogId { get; set; }

        [StringLength(50)]
        public string? DiscountSchemeId { get; set; }

        public decimal? ItemSubTotal { get; set; }

        [StringLength(50)]
        public string? OrderId { get; set; }

        [StringLength(50)]
        public string? CartId { get; set; }

        public DateTime? CreatedBefore { get; set; }

        public DateTime? CreatedAfter { get; set; }
    }
}
