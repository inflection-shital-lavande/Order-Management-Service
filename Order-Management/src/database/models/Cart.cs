using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace order_management.database.models;

public class Cart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [StringLength(36)]
    public Guid? CustomerId { get; set; }

    public int? TotalItemsCount { get; set; } = 0;

    public float? TotalTax { get; set; } = 0.0f;

    public float? TotalDiscount { get; set; } = 0.0f;

    public float? TotalAmount { get; set; } = 0.0f;

    public DateTime? CartToOrderTimestamp { get; set; }

    [StringLength(36)]
    public Guid? AssociatedOrderId { get; set; }

   // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? CreatedAt { get; set; }

   // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    //one to many  cart and order 
    public ICollection<Order> Orders { get; set; }

    //one to many cart and orderlineitem
    public ICollection<OrderLineItem> OrderLineItems { get; set; } = new List<OrderLineItem>();


    
    public Cart()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
