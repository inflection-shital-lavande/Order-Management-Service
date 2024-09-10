using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace order_management.database.dto;

public class AddressResponseModel
{
    
    [Display(Description = "ID of the address.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "AddressLine1 is required.")]
    [MinLength(2, ErrorMessage = "AddressLine1 must be at least 2 characters long.")]
    [MaxLength(512, ErrorMessage = "AddressLine1 cannot exceed 512 characters.")]
    [Display(Description = "First line of the address.")]
    public string? AddressLine1 { get; set; }

    
    [MinLength(2, ErrorMessage = "AddressLine2 must be at least 2 characters long.")]
    [MaxLength(512, ErrorMessage = "AddressLine2 cannot exceed 512 characters.")]
    [Display(Description = "Second line of the address.")]
    public string? AddressLine2 { get; set; }

    
    [Required(ErrorMessage = "City is required.")]
    [MinLength(2, ErrorMessage = "City must be at least 2 characters long.")]
    [MaxLength(64, ErrorMessage = "City cannot exceed 64 characters.")]
    [Display(Description = "City of the address.")]
    public string? City { get; set; }

 
    [MinLength(2, ErrorMessage = "State must be at least 2 characters long.")]
    [MaxLength(64, ErrorMessage = "State cannot exceed 64 characters.")]
    [Display(Description = "State of the address.")]
    public string? State { get; set; }

    
    [MinLength(2, ErrorMessage = "Country must be at least 2 characters long.")]
    [MaxLength(32, ErrorMessage = "Country cannot exceed 32 characters.")]
    [Display(Description = "Country of the address.")]
    public string? Country { get; set; }

  
    [MinLength(2, ErrorMessage = "ZipCode must be at least 2 characters long.")]
    [MaxLength(32, ErrorMessage = "ZipCode cannot exceed 32 characters.")]
    [Display(Description = "Zip code of the address.")]
    public string? ZipCode { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}


