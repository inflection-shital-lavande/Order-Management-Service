using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace order_management.database.dto;

public class CustomerUpdateModel
{
    [StringLength(128)]
    public string? Name { get; set; }

    [StringLength(512)]
    public string? Email { get; set; }

    [StringLength(8)]
    public string? PhoneCode { get; set; }
   
    public string? Phone { get; set; }

    public string? ProfilePicture { get; set; }
    [StringLength(64)]
    public string? TaxNumber { get; set; }
}




