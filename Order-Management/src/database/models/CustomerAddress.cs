using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using order_management.domain_types.enums;


namespace order_management.database.models;

public class CustomerAddress
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [StringLength(36)]
    [ForeignKey("CustomerId")]

    public Guid? CustomerId { get; internal set; }

    [StringLength(36)]
    [ForeignKey("AddressId")]

    public Guid? AddressId { get; internal set; }

    public AddressTypes AddressType { get;  internal set; }// = AddressTypes.SHIPPING;

    public bool? IsFavorite { get; internal set; } = false;

    public virtual Customer Customers { get; set; }

   public virtual Address Addresses { get; set; }

   
}

