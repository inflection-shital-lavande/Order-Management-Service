namespace Order_Management.src.database.dto.OrderCoupan
{
    public class OrderCoupanCreateDTO
    {
        public string Code { get; set; }
        public Guid CouponId { get; set; }
        public Guid OrderId { get; set; }
        public float DiscountValue { get; set; } = 0.00f;
        public float DiscountPercentage { get; set; } = 0.00f;
        public float DiscountMaxAmount { get; set; } = 0.00f;
        public bool Applied { get; set; } = true;
        public DateTime? AppliedAt { get; set; }
    }
}
