using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace order_management.src.database.dto;

public class OrderUpdateModel
{
    [Required]
    [Description("Id of the order type")]
    public Guid? OrderTypeId { get; set; }//OrderTypeId

    [Required]
    [Description("Id of the cart")]
    public Guid? AssociatedCartId { get; set; }

    public double? OrderDiscount { get; set; } = 0.0;

    [Description("Tip applicable or not")]
    public bool? TipApplicable { get; set; }

    public double? TipAmount { get; set; } = 0.0;
   
     public string? Notes { get; set; }
}
