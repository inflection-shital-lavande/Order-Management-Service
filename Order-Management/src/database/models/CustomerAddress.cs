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

    public AddressTypes AddressType { get;  internal set; } = AddressTypes.SHIPPING;

    public bool? IsFavorite { get; internal set; } = false;

    public virtual Customer? Customers { get; set; }

   public virtual Address? Addresses { get; set; }

   
}
//eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJGdWxsbmFtZSI6IlZhaXNobmF2aSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJ2YWlzaHU1NUBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ2YWlzaHU1NUBnbWFpbC5jb20iLCJleHAiOjE3MjYxMTgwODIsImlzcyI6Imh0dHBzOi8veW91cmRvbWFpbi5jb20iLCJhdWQiOiJodHRwczovL3lvdXJhcGkueW91cmRvbWFpbi5jb20ifQ.R_rrQuz-WLCZgqtn0guAw5Fc8BPGYP22T0MmMK6nUJI

