using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace order_management.database.dto
{
    public class AddressUpdateModel
    {
        [MaxLength(512)]
        [Required]
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        [Required]
        public string? City { get; set; }
        public string? State { get; set; }
        [Required]
        [Display(Description = "Country of the address.")] 
        public string? Country { get; set; }
       
        public string? ZipCode { get; set; }
    }
}

