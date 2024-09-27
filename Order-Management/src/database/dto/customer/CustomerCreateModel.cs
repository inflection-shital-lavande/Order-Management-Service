using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace order_management.database.dto
{
    public class CustomerCreateModel
    {


       [StringLength(36)]
        [Required]
        public Guid? ReferenceId { get; set; }
        [Required]
        [StringLength(128)]
        public string? Name { get; set; }

        [StringLength(512)]
        public string? Email { get; set; }

        public string? PhoneCode { get; set; }

        public string? Phone { get; set; }

       
        public string? ProfilePicture { get; set; }

       
        public string? TaxNumber { get; set; }

        [StringLength(36)]
       
        public Guid? DefaultShippingAddressId { get; set; }

        [StringLength(36)]
        
        public Guid? DefaultBillingAddressId { get; set; }
    }
}
