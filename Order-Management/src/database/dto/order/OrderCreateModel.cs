using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace order_management.src.database.dto;

public class OrderCreateModel
{
    [StringLength(36)]
    [Required]
    public Guid? OrderTypeId { get; set; }

    [Required]
    public Guid? CustomerId { get; set; }
    [Required]
   // [StringLength(36)]
    public Guid? AssociatedCartId { get; set; }

   
    public bool? TipApplicable { get; set; } = false;
   // [StringLength(1024)]
    public string? Notes { get; set; }
}


