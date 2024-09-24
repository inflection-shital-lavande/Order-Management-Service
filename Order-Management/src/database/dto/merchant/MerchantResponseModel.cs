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

       
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Logo { get; set; }

        
        public string? WebsiteUrl { get; set; }

        
        public string? TaxNumber { get; set; }

       
        public string? GSTNumber { get; set; }

        [Display(Description = "Address Id of the merchant")]

        public Dictionary<string, object>? Addressess { get; set; }
       
        public Guid? AddressId { get; set; }

        [Display(Description = "Created at")]
        public DateTime? CreatedAt { get; set; }

        [Display(Description = "Updated at")]
        public DateTime? UpdatedAt { get; set; }
    }
}


