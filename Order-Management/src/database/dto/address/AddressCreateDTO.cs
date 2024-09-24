using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations;

namespace order_management.database.dto;

public class AddressCreateModel
{

    [Required]
    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    [Required]
    public string? City { get; set; }
   
    public string? State { get; set; }
   
    public string? Country { get; set; }
   
    public string? ZipCode { get; set; }
    [Display(Description = "Zip code of the address.")]
    public Guid? CreatedBy { get; set; }
}




