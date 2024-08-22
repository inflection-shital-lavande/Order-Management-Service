using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace order_management.database.models;

public class OrderType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string? Name { get; set; }

    [MaxLength(64)]
    public string? Description { get; set; }

    [Required]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    //one  to many 
    public ICollection<Order> Orders { get; set; }
  
}

