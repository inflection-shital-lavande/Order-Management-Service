using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.merchant
{
    public class MerchantUpdateModel
    {

        [StringLength(512)]
         public string? Name { get; set; }
        [StringLength(512)]
        public string? Email { get; set; }
        [StringLength(12)]
        public string? Phone { get; set; }

        [StringLength(512)]
        public string? Logo { get; set; }
        [StringLength(64)]
        public string? TaxNumber { get; set; }

        [StringLength(64)]
        public string? GSTNumber { get; set; }

        public Guid? AddressId { get; set; }
    }
}



