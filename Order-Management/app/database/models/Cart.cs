using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace Order_Management.app.database.models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(36)]
        public string CustomerId { get; set; }

        public int TotalItemsCount { get; set; } = 0;

        public float TotalTax { get; set; } = 0.0f;

        public float TotalDiscount { get; set; } = 0.0f;

        public float TotalAmount { get; set; } = 0.0f;

        public DateTime? CartToOrderTimestamp { get; set; }

        [StringLength(36)]
        public string AssociatedOrderId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public Cart()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        
    }

}
