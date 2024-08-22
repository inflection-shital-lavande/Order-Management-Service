using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace order_management.database.models;

public partial class Address
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "AddressLine1 is required")]
    [MaxLength(512)]
    public string? AddressLine1 { get; set; }

    [MaxLength(512)]
    public string? AddressLine2 { get; set; }

    [Required(ErrorMessage = "City is required")]
    [MaxLength(5)]
    public string? City { get; set; }

    [Required]
    [MaxLength(64)]
    public string? State { get; set; }
    [MaxLength(32)]
    public string? Country { get; set; }

    [MaxLength(32)]

    public string? ZipCode { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    

    //many to many 
    public ICollection<CustomerAddress> CustomerAddresses { get; set; }

    //one to many Address and order
    [NotMapped]
    public ICollection<Order> Orders { get; set; }
     public ICollection<Merchant> Merchants { get; set; }


    public Address()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

}
