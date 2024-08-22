using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.merchant
{
    public class MerchantUpdateModel
    {
        [MinLength(5, ErrorMessage = "Name must be at least 5 characters long")]
        [MaxLength(512, ErrorMessage = "Name can be a maximum of 512 characters")]
        [Display(Description = "Name of the merchant")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Email should be a valid email address")]
        [MinLength(5, ErrorMessage = "Email must be at least 5 characters long")]
        [MaxLength(512, ErrorMessage = "Email can be a maximum of 512 characters")]
        [Display(Description = "Email of the merchant")]
        public string? Email { get; set; }

        [MinLength(2, ErrorMessage = "Phone number must be at least 2 characters long")]
        [MaxLength(12, ErrorMessage = "Phone number can be a maximum of 12 characters")]
        [Display(Description = "Phone number of the merchant")]
        public string? Phone { get; set; }

        [MinLength(5, ErrorMessage = "Logo URL must be at least 5 characters long")]
        [MaxLength(512, ErrorMessage = "Logo URL can be a maximum of 512 characters")]
        [Display(Description = "Logo URL of the merchant")]
        public string? Logo { get; set; }

        [MinLength(2, ErrorMessage = "Tax number/code must be at least 2 characters long")]
        [MaxLength(64, ErrorMessage = "Tax number/code can be a maximum of 64 characters")]
        [Display(Description = "Tax number/code of the merchant")]
        public string? TaxNumber { get; set; }

        [MinLength(2, ErrorMessage = "GST number/code must be at least 2 characters long")]
        [MaxLength(64, ErrorMessage = "GST number/code can be a maximum of 64 characters")]
        [Display(Description = "GST number/code of the merchant")]
        public string? GSTNumber { get; set; }

        [Display(Description = "Address Id of the merchant")]
        public Guid? AddressId { get; set; }
    }
}



