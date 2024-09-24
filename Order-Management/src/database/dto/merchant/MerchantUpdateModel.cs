using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.merchant
{
    public class MerchantUpdateModel
    {
        [Required]
        [Display(Description = "Name of the merchant")]
        public string? Name { get; set; }

        [Required]
        [Display(Description = "Email of the merchant")]
        public string? Email { get; set; }

        [Required]
        [Display(Description = "Phone number of the merchant")]
        public string? Phone { get; set; }

       
        [Display(Description = "Logo URL of the merchant")]
        public string? Logo { get; set; }

       
        [Display(Description = "Tax number/code of the merchant")]
        public string? TaxNumber { get; set; }

       
        [Display(Description = "GST number/code of the merchant")]
        public string? GSTNumber { get; set; }

       
        [Display(Description = "Address Id of the merchant")]
        public Guid? AddressId { get; set; }
    }
}



