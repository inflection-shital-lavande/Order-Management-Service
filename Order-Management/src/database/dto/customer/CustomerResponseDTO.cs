using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace order_management.database.dto;

public class CustomerResponseModel
{
    [Display(Description = "Id of the customer")]

    public Guid Id { get; set; }
    [Display(Description = "Reference Id of the customer in the customer service")]

    public Guid? ReferenceId { get; set; }
    [Display(Description = "Name of the customer")]

    public string? Name { get; set; }
    [Display(Description = "Email of the customer")]

    public string? Email { get; set; }
    [Display(Description = "Phone code of the customer")]

    public string? PhoneCode { get; set; }
    [Display(Description = "Phone number of the customer")]

    public string? Phone { get; set; }
    [Display(Description = "Profile picture URL of the customer")]

    public string? ProfilePicture { get; set; }
    [Display(Description = "Tax number/code of the customer")]

    public string? TaxNumber { get; set; }
    [Display(Description = "Shipping address Id of the customer")]

    public Guid? DefaultShippingAddressId { get; set; }
    [Display(Description = "Shipping address Id of the customer")]

    public Dictionary<string, object>? DefaultShippingAddress { get; set; }
    [Display(Description = "Billing address Id of the customer")]

    public Guid? DefaultBillingAddressId { get; set; }
    [Display(Description = "Billing address of the customer")]

    public Dictionary<string, object>? DefaultBillingAddress { get; set; }
    [Display(Description = "Created at")]

    public DateTime? CreatedAt { get; set; }
    [Display(Description = "Updated at")]

    public DateTime? UpdatedAt { get; set; }
}




   
   
    