using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.order_line_item
{
    public class OrderLineItemUpdateModel
    {
        [StringLength(100)]
        public string? Name { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? Discount { get; set; }

        [StringLength(50)]
        public string? DiscountSchemeId { get; set; }

        public decimal? Tax { get; set; }

        public decimal? ItemSubTotal { get; set; }

        [StringLength(50)]
        public string? OrderId { get; set; }

        [StringLength(50)]
        public string? CartId { get; set; }
    }
}
