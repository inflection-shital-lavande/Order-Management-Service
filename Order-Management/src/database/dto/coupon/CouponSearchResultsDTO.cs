using System.ComponentModel.DataAnnotations;

namespace order_management.database.dto;

public class CouponSearchResultsModel
{
    [Display(Description = "List of coupons")]

    public List<CouponResponseModel> Items { get; set; }
}
