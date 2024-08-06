namespace Order_Management.database.dto
{
    public class CustomerSearchFilterDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneCode { get; set; }
        public string? Phone { get; set; }
        public string? TaxNumber { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public int? PastMonths { get; set; }
    }
}
