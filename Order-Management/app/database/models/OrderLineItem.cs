using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.app.database.models
{
    public class OrderLineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(512)]
        public string Name { get; set; }

        [MaxLength(36)]
        public string CatalogId { get; set; }

        [Required]
        public int Quantity { get; set; } = 0;

        [Required]
        public double UnitPrice { get; set; } = 0.0;

        [Required]
        public double Discount { get; set; } = 0.0;

        [MaxLength(36)]
        public string DiscountSchemeId { get; set; }

        [Required]
        public double Tax { get; set; } = 0.0;

        [Required]
        public double ItemSubTotal { get; set; } = 0.0;

        [Required]
        [MaxLength(36)]
        public string OrderId { get; set; }

        [MaxLength(36)]
        public string CartId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        

        public OrderLineItem(Guid id, string name, int quantity, double unitPrice, double discount, double tax, double itemSubTotal,
                             string orderId, string catalogId = null, string discountSchemeId = null, string cartId = null)
        {
            Id = id;
            Name = name;
            CatalogId = catalogId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            DiscountSchemeId = discountSchemeId;
            Tax = tax;
            ItemSubTotal = itemSubTotal;
            OrderId = orderId;
            CartId = cartId;
        }

    }
   }
