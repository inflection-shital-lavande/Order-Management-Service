using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using order_management.domain_types.enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json.Serialization;

namespace order_management.database.models;


public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [MaxLength(64)]
    public string? DisplayCode { get; set; }

    [Required]
    public OrderStatusTypes OrderStatus { get; set; } = OrderStatusTypes.DRAFT;

    [MaxLength(64)]
    public string? InvoiceNumber { get; set; }
    [ForeignKey("AssociatedCartId")]
    public Guid? AssociatedCartId { get; set; }

    public int? TotalItemsCount { get; set; } = 0;

    public double? OrderDiscount { get; set; } = 0.0;

    public bool? TipApplicable { get; set; } = false;

    public double? TipAmount { get; set; } = 0.0;

    public double? TotalTax { get; set; } = 0.0;

    public double? TotalDiscount { get; set; } = 0.0;

    public double? TotalAmount { get; set; } = 0.0;

    [MaxLength(1024)]
    public string? Notes { get; set; }

    [ForeignKey("CustomerId")]
    public Guid? CustomerId { get; set; }

    [ForeignKey("ShippingAddressId")]
    public Guid? ShippingAddressId { get; set; }

    [ForeignKey("BillingAddressId")]
    public Guid? BillingAddressId { get; set; }

    [ForeignKey("OrderTypeId")]
    public Guid? OrderTypeId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    [JsonIgnore]
    public virtual Cart Carts { get; set; }
    [JsonIgnore]
    public virtual Customer Customers { get; set; }
    [JsonIgnore]
    public virtual Address ShippingAddress { get; set; }
    [JsonIgnore]
    public virtual Address BillingAddress { get; set; }
    [JsonIgnore]
    public virtual OrderType OrderTypes { get; set; }
    [JsonIgnore]

    public virtual OrderHistory OrderHistorys { get; set; }

    public ICollection<OrderCoupon> OrderCoupons { get; set; }
    public ICollection<PaymentTransaction> PaymentTransactions { get; set; }
    public ICollection<OrderLineItem> OrderLineItems { get; set; } = new List<OrderLineItem>();
}