using System.ComponentModel.DataAnnotations;

namespace order_management.database.dto;

public class AddressSearchResultsModel
{
    [Display(Description = "List of addresses matching the search criteria.")]
    public List<AddressResponseModel> Items { get; set; }
}
