using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.order_line_item
{
    public class OrderLineItemUpdateModel
    {

       
        [StringLength(512)]
        public string? Name { get; set; }
       
        public int? Quantity { get; set; }
       
        public double? UnitPrice { get; set; }
        
        public double? Discount { get; set; }
        [MaxLength(36)]
        public Guid? DiscountSchemeId { get; set; }
       
        public double? Tax { get; set; }
        
        public double? ItemSubTotal { get; set; }
       
        public Guid? OrderId { get; set; }
       
        public Guid? CartId { get; set; }

       
    }
}
