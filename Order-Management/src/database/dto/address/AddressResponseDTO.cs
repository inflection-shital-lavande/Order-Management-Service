using System.ComponentModel.DataAnnotations;

namespace Order_Management.database.dto
{
    public class AddressResponseDTO
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(512)]
        public string? AddressLine1 { get; set; }
        [MaxLength(512)]
        public string? AddressLine2 { get; set; }

        [Required]
        [MaxLength(64)]
        public string? City { get; set; }
        [MaxLength(64)]
        public string? State { get; set; }
        [MaxLength(32)]
        public string? Country { get; set; }
        [MaxLength(32)]
        public string? ZipCode { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
