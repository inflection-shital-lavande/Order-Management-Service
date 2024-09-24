using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.cart
{
    public class CartUpdateModel
    {
        [Required]
        public int? TotalItemsCount { get; set; }
        
        public float? TotalTax { get; set; }
        
        public float? TotalDiscount { get; set; }

        [Required]
        public float? TotalAmount { get; set; }

    }
}
