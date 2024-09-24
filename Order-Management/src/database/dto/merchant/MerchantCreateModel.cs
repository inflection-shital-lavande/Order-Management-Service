using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.merchant
{
    public class MerchantCreateModel
    {
        [Required]
        public Guid? ReferenceId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Description = "Name of the merchant")]
        public string? Name { get; set; }

        [Required]
        [Display(Description = "Email of the merchant")]
        public string? Email { get; set; }

        [Required]
        [Display(Description = "Phone number of the merchant")]
        public string? Phone { get; set; }

        public string? Logo { get; set; }

        public string? WebsiteUrl { get; set; }
        [Display(Description = "Tax number/code of the merchant")]
        public string? TaxNumber { get; set; }

        [Display(Description = "GST number/code of the merchant")]
        public string? GSTNumber { get; set; }

        [Required]
        [Display(Description = "Address Id of the merchant")]
        public Guid? AddressId { get; set; }
    }
}



