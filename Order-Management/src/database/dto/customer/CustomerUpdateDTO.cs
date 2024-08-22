using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace order_management.database.dto;

public class CustomerUpdateModel
{
    [StringLength(128, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 128 characters.")]
    [Description("Name of the customer")]
    public string? Name { get; set; }

    [StringLength(512, MinimumLength = 5, ErrorMessage = "Email must be between 5 and 512 characters.")]
    [Description("Email of the customer")]
    public string? Email { get; set; }

    [StringLength(8, MinimumLength = 1, ErrorMessage = "Phone code must be between 1 and 8 characters.")]
    [Description("Phone code of the customer")]
    public string? PhoneCode { get; set; }

    [StringLength(12, MinimumLength = 2, ErrorMessage = "Phone number must be between 2 and 12 characters.")]
    [Description("Phone number of the customer")]
    public string? Phone { get; set; }

    [StringLength(512, MinimumLength = 5, ErrorMessage = "Profile picture URL must be between 5 and 512 characters.")]
    [Description("Profile picture URL of the customer")]
    public string? ProfilePicture { get; set; }

    [StringLength(64, MinimumLength = 2, ErrorMessage = "Tax number must be between 2 and 64 characters.")]
    [Description("Tax number/code of the customer")]
    public string? TaxNumber { get; set; }
}




