using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.cart
{
    public class CartUpdateModel
    {
        [Required]
        public int? TotalItemsCount { get; set; }

        public double? TotalTax { get; set; }

        public double? TotalDiscount { get; set; }

        public double? TotalAmount { get; set; }
    }
}
