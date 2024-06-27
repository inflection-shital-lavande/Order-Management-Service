using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Order_Management.app.domain_types;
using Order_Management.app.domain_types.enums;

namespace Order_Management.app.database.models
{
    public class CustomerAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(36)]
        public Guid CustomerId { get; set; }

        [StringLength(36)]
        public Guid AddressId { get; set; }

        public AddressTypes AddressType { get; set; } = AddressTypes.SHIPPING;

        public bool IsFavorite { get; set; } = false;

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        public CustomerAddress()
        {
            Id = Guid.NewGuid();
        }
    }
  }
