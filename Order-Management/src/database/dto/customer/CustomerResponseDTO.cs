namespace Order_Management.database.dto
{
    public class CustomerResponseDTO
    {
        public Guid Id { get; set; }
        public Guid? ReferenceId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneCode { get; set; }
        public string? Phone { get; set; }
        public string? ProfilePicture { get; set; }
        public string? TaxNumber { get; set; }
        public Guid? DefaultShippingAddressId { get; set; }
        public Dictionary<string, object>? DefaultShippingAddress { get; set; }
        public Guid? DefaultBillingAddressId { get; set; }
        public Dictionary<string, object>? DefaultBillingAddress { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
