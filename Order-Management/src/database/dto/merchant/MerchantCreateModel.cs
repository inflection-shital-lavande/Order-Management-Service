using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.merchant
{
    public class MerchantCreateModel
    {
        [Required]
        public Guid? ReferenceId { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Logo { get; set; }

        public string? WebsiteUrl { get; set; }
     
        public string? TaxNumber { get; set; }

        public string? GSTNumber { get; set; }

        public Guid? AddressId { get; set; }
    }
}



