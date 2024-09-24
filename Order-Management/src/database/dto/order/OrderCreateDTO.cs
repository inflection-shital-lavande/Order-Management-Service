using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace order_management.src.database.dto;

public class OrderCreateModel
{
    [Required]
    [Description("Id of the order type")]
    public Guid? OrderTypeId { get; set; }

    
    [Required(ErrorMessage = "Customer Id is required.")]
    public Guid? CustomerId { get; set; }


    [Required(ErrorMessage = "AssociatedCart Id is required.")]
    public Guid? AssociatedCartId { get; set; }

    [Description("Tip applicable or not")]
    public bool? TipApplicable { get; set; } = false;

    public string? Notes { get; set; }
}


