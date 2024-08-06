using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.database.models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(36)]
      
        public Guid? ReferenceId { get; set; }

        [StringLength(128)]
        public string? Name { get; set; }

        [StringLength(512)]
       
        public string? Email { get; set; }

        [StringLength(8)]
        public string? PhoneCode { get; set; }

        [StringLength(64)]
       
        public string? Phone { get; set; }

        [StringLength(512)]
        public string? ProfilePicture { get; set; }

        [StringLength(64)]
        
        public string? TaxNumber { get; set; }

        [StringLength(36)]
        public Guid? DefaultShippingAddressId { get; set; }

        [StringLength(36)]
        public Guid? DefaultBillingAddressId { get; set; }

        [ForeignKey("DefaultShippingAddressId")]
        public virtual Address DefaultShippingAddress { get; set; }

        [ForeignKey("DefaultBillingAddressId")]
        public virtual Address DefaultBillingAddress { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedAt { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedAt { get; set; }

        public ICollection<CustomerAddress> CustomerAddresses { get; set; }

        //one to many customer and order 
       // public ICollection<Order> Orders { get; set; }

       // public ICollection<PaymentTransaction> PaymentTransactions { get; set; }
       
       
    }
}
