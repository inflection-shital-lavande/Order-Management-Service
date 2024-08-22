using System.ComponentModel.DataAnnotations;

namespace order_management.database.dto;

public class AddressSearchFilterModel
{
    [MinLength(2, ErrorMessage = "AddressLine1 must be at least 2 characters long.")]
    [MaxLength(512, ErrorMessage = "AddressLine1 cannot exceed 512 characters.")]
    [Display(Description = "Filter by the first line of the address.")]
    public string? AddressLine1 { get; set; }

    [MinLength(2, ErrorMessage = "AddressLine2 must be at least 2 characters long.")]
    [MaxLength(512, ErrorMessage = "AddressLine2 cannot exceed 512 characters.")]
    [Display(Description = "Filter by the second line of the address.")]
    public string? AddressLine2 { get; set; }

    [MinLength(2, ErrorMessage = "City must be at least 2 characters long.")]
    [MaxLength(64, ErrorMessage = "City cannot exceed 64 characters.")]
    [Display(Description = "Filter by the city of the address.")]
    public string? City { get; set; }

    [MinLength(2, ErrorMessage = "State must be at least 2 characters long.")]
    [MaxLength(64, ErrorMessage = "State cannot exceed 64 characters.")]
    [Display(Description = "Filter by the state of the address.")]
    public string? State { get; set; }

    [MinLength(2, ErrorMessage = "Country must be at least 2 characters long.")]
    [MaxLength(32, ErrorMessage = "Country cannot exceed 32 characters.")]
    [Display(Description = "Filter by the country of the address.")]
    public string? Country { get; set; }

    [MinLength(2, ErrorMessage = "ZipCode must be at least 2 characters long.")]
    [MaxLength(32, ErrorMessage = "ZipCode cannot exceed 32 characters.")]
    [Display(Description = "Filter by the zip code of the address.")]
    public string? ZipCode { get; set; }
}


