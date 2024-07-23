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
        public Guid? CatalogId { get; set; }

        [Required]
        public int Quantity { get; set; } = 0;

        [Required]
        public double UnitPrice { get; set; } = 0.0;

        [Required]
        public double Discount { get; set; } = 0.0;

        [MaxLength(36)]
        public Guid? DiscountSchemeId { get; set; }

        [Required]
        public double Tax { get; set; } = 0.0;

        [Required]
        public double ItemSubTotal { get; set; } = 0.0;

        [Required]
        [MaxLength(36)]
        public Guid? OrderId { get; set; }

        [MaxLength(36)]
        public Guid? CartId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }


        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart> Carts { get; set; }

    }
   }
