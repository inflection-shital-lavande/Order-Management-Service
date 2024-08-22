using System.ComponentModel;

namespace order_management.src.database.dto;

public class OrderSearchResultsModel
{
    [Description("List of orders")]
    public List<OrderResponseModel> Items { get; set; } = new List<OrderResponseModel>();
}
