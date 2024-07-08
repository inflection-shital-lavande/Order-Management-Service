using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using Order_Management.app.domain_types.enums;

namespace Order_Management.app.database.models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(36)]
        public Guid? DisplayCode { get; set; }

        [Required]
        public OrderStatusTypes OrderStatus { get; set; } = OrderStatusTypes.DRAFT;

        [Required]
        [MaxLength(64)]
        public string InvoiceNumber { get; set; }

        public Guid? AssociatedCartId { get; set; }

        public int TotalItemsCount { get; set; } = 0;

        public double OrderDiscount { get; set; } = 0.0;

        public bool TipApplicable { get; set; } = false;

        public double TipAmount { get; set; } = 0.0;

        public double TotalTax { get; set; } = 0.0;

        public double TotalDiscount { get; set; } = 0.0;

        public double TotalAmount { get; set; } = 0.0;

        [MaxLength(1024)]
        public string Notes { get; set; }

        public Guid? CustomerId { get; set; }

        public Guid? ShippingAddressId { get; set; }

        public Guid? BillingAddressId { get; set; }

        public Guid? OrderType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public ICollection<OrderCoupon> orderCoupons { get; set; }
    }
   }
