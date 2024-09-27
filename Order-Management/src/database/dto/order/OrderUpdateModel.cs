using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace order_management.src.database.dto;

public class OrderUpdateModel
{
    [Required]
    public Guid? OrderTypeId { get; set; }

    public Guid? AssociatedCartId { get; set; }

    [Range(0, double.MaxValue)]
    public double? OrderDiscount { get; set; } = 0.0;

    public bool? TipApplicable { get; set; }

    [Range(0, double.MaxValue)]
    public double? TipAmount { get; set; } = 0.0;

    [StringLength(1024)]
    public string? Notes { get; set; }
}
