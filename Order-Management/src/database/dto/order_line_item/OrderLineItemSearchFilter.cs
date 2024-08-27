using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.order_line_item
{
    public class OrderLineItemSearchFilter
    {
        [StringLength(100)]
        public string? Name { get; set; }

        public Guid? CatalogId { get; set; }

        public Guid? DiscountSchemeId { get; set; }

        public double? ItemSubTotal { get; set; }

        public Guid? OrderId { get; set; }

        public Guid? CartId { get; set; }

        public DateTime? CreatedBefore { get; set; }

        public DateTime? CreatedAfter { get; set; }
    }
}
