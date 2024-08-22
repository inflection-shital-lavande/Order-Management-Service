using System.ComponentModel.DataAnnotations;

namespace order_management.database.dto;

public class CustomerSearchFilterModel
{
    [Display(Description = "Search by the name of the customer")]
    public string? Name { get; set; }
    [Display(Description = "Search by the email of the customer")]

    public string? Email { get; set; }
    [Display(Description = "Search by the phone code of the customer")]

    public string? PhoneCode { get; set; }
    [Display(Description = "Search by the phone number of the customer")]

    public string? Phone { get; set; }
    [Display(Description = "Search by the tax number/code of the customer")]

    public string? TaxNumber { get; set; }
    [Display(Description = "Search customers created before the given date")]

    public DateTime? CreatedBefore { get; set; }
    [Display(Description = "Search customers created after the given date")]

    public DateTime? CreatedAfter { get; set; }
    [Display(Description = "Search customers created in the past given number of months")]

    public int? PastMonths { get; set; }
}



