using System.ComponentModel.DataAnnotations;

namespace Order_Management.database.dto
{
    public class AddressCreateDTO
    {
        [Required]
        [MaxLength(512)]
        public string? AddressLine1 { get; set; }

        [MaxLength(512)]
        public string? AddressLine2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MaxLength(64)]
        public string? City { get; set; }

        [MaxLength(64)]
        public string? State { get; set; }
        [MaxLength(32)]
        public string? Country { get; set; }
        [MaxLength(32)]

        public string? ZipCode { get; set; }

        public string? CreatedBy { get; set; }
    }
}
