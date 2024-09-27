using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace order_management.database.dto;

public class AddressResponseModel
{
    
    [Display(Description = "ID of the address.")]
    public Guid Id { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }
    public string? State { get; set; }

    public string? Country { get; set; }
    public string? ZipCode { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}


