using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace order_management.database.dto
{
    public class CustomerCreateModel
    {
        [StringLength(36)]
        [Required(ErrorMessage = "Reference Id is required.")]
        
        public Guid? ReferenceId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }

        public string? PhoneCode { get; set; }

        [Required]
        public string? Phone { get; set; }

        public string? ProfilePicture { get; set; }

        [Description("Tax number/code of the customer")]
       
        public string? TaxNumber { get; set; }

        [StringLength(36)]
       
        public Guid? DefaultShippingAddressId { get; set; }

        [StringLength(36)]
        //[Display(Description = "Billing address Id of the customer")]
        //[Required]
        public Guid? DefaultBillingAddressId { get; set; }
    }
}
