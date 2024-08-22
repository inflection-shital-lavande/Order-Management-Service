using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.merchant
{
    public class MerchantResponseModel
    {
        [Display(Description = "Id of the merchant")]
        public Guid Id { get; set; }

        [Display(Description = "Reference Id of the merchant in the merchant service")]
        public Guid? ReferenceId { get; set; }

        [Required, StringLength(512, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 512 characters.")]
        [Display(Description = "Name of the merchant")]
        public string? Name { get; set; }

        [StringLength(512, MinimumLength = 5, ErrorMessage = "Email must be between 5 and 512 characters.")]
        [Display(Description = "Email of the merchant")]
        public string? Email { get; set; }

        [StringLength(12, MinimumLength = 2, ErrorMessage = "Phone must be between 2 and 12 characters.")]
        [Display(Description = "Phone number of the merchant")]
        public string? Phone { get; set; }

        [StringLength(512, MinimumLength = 5, ErrorMessage = "Logo URL must be between 5 and 512 characters.")]
        [Display(Description = "Logo URL of the merchant")]
        public string? Logo { get; set; }

        [StringLength(512, MinimumLength = 5, ErrorMessage = "Website URL must be between 5 and 512 characters.")]
        [Display(Description = "Website URL of the merchant")]
        public string? WebsiteUrl { get; set; }

        [StringLength(64, MinimumLength = 2, ErrorMessage = "Tax number must be between 2 and 64 characters.")]
        [Display(Description = "Tax number/code of the merchant")]
        public string? TaxNumber { get; set; }

        [StringLength(64, MinimumLength = 2, ErrorMessage = "GST number must be between 2 and 64 characters.")]
        [Display(Description = "GST number/code of the merchant")]
        public string? GSTNumber { get; set; }

        [Display(Description = "Address Id of the merchant")]
        public Guid? AddressId { get; set; }

        [Display(Description = "Created at")]
        public DateTime? CreatedAt { get; set; }

        [Display(Description = "Updated at")]
        public DateTime? UpdatedAt { get; set; }
    }
}


