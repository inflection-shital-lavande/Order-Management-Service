using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.order_line_item
{
    public class OrderLineItemUpdateModel
    {

        [Required]  
        [StringLength(100)]
        public string? Name { get; set; }
        [Required]

        public int? Quantity { get; set; }
        [Required]

        public double? UnitPrice { get; set; }
        [Required]

        public double? Discount { get; set; }
        [Required]

        public Guid? DiscountSchemeId { get; set; }
       

        public double? Tax { get; set; }
        

        public double? ItemSubTotal { get; set; }
        [Required(ErrorMessage = "OrderId is required.")]

        public Guid? OrderId { get; set; }
        [Required(ErrorMessage = "CartId is required.")]

        public Guid? CartId { get; set; }

       
    }
}
