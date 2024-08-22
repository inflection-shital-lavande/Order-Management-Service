using System;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.merchant
{
    public class MerchantSearchFilter
    {
        [Display(Description = "Search by the name of the merchant")]
        public string? Name { get; set; }

        [Display(Description = "Search by the email of the merchant")]
        public string? Email { get; set; }

        [Display(Description = "Search by the phone number of the merchant")]
        public string? Phone { get; set; }

        [Display(Description = "Search by the tax number/code of the merchant")]
        public string? TaxNumber { get; set; }

        [Display(Description = "Search merchants created before the given date")]
        public DateTime? CreatedBefore { get; set; }

        [Display(Description = "Search merchants created after the given date")]
        public DateTime? CreatedAfter { get; set; }

        [Range(0, 12, ErrorMessage = "PastMonths must be between 0 and 12.")]
        [Display(Description = "Search merchants created in the past given number of months")]
        public int? PastMonths { get; set; }

        

    }
}





