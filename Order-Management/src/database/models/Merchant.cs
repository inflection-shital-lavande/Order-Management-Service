using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.database.models
{
    public class Merchant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(36)]
        public Guid? ReferenceId { get; set; }

        [StringLength(512)]
        public string Name { get; set; }

        [StringLength(512)]
        public string Email { get; set; }

        [StringLength(64)]
        public string Phone { get; set; }

        [StringLength(512)]
        public string Logo { get; set; }

        [StringLength(512)]
        public string WebsiteUrl { get; set; }

        [StringLength(64)]
        public string TaxNumber { get; set; }

        [StringLength(64)]
        public string GSTNumber { get; set; }

        [StringLength(36)]
        public Guid? AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedAt { get; set; }

        


    }
}
