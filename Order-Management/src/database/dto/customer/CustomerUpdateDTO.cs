using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace order_management.database.dto;

public class CustomerUpdateModel
{
    [Required]
    [Description("Name of the customer")]
    public string? Name { get; set; }

    
    [Description("Email of the customer")]
    [Required]
    public string? Email { get; set; }

   
    [Description("Phone code of the customer")]
    [Required]
    public string? PhoneCode { get; set; }

    [Required]
    [Description("Phone number of the customer")]
    public string? Phone { get; set; }

    
    [Description("Profile picture URL of the customer")]
    public string? ProfilePicture { get; set; }
 
    [Description("Tax number/code of the customer")]
    public string? TaxNumber { get; set; }
}




