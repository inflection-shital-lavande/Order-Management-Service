using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace order_management.src.database.dto;

public class OrderCreateModel
{
    [Description("Id of the order type")]
    public Guid? OrderTypeId { get; set; }

    [Required(ErrorMessage = "Customer Id is required.")]
    [Description("Id of the customer")]
    public Guid? CustomerId { get; set; }

    [Description("Id of the cart")]
    public Guid? AssociatedCartId { get; set; }

    [Description("Tip applicable or not")]
    public bool? TipApplicable { get; set; } = false;

    [StringLength(1024, MinimumLength = 5, ErrorMessage = "Notes must be between 5 and 1024 characters.")]
    [Description("Notes for the delivery")]
    public string? Notes { get; set; }
}


