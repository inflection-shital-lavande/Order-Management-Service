using System.ComponentModel.DataAnnotations;

namespace order_management.database.dto;

public class CustomerSearchResultsModel
{
    [Display(Description = "List of customers")]

    public List<CustomerResponseModel> Items { get; set; }

}


