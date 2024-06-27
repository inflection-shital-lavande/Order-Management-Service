using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace Order_Management.app.database.models
{
    public partial class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(512)]
        public string? AddressLine1 { get; set; }

        [MaxLength(512)]
        public string? AddressLine2 { get; set; }

        [Required]
        [MaxLength(64)]
        public string? City { get; set; }

        [MaxLength(64)]
        public string? State { get; set; }
        [MaxLength(32)]
        public string? Country { get; set; }
        [MaxLength(32)]

        public string? ZipCode { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ICollection<CustomerAddress> customerAddresses { get; set; }
        public Address()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        
    }

}


