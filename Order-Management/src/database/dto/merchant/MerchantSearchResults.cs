using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.merchant
{
    public class MerchantSearchResults
    {
        //[Display(Description = "")]
        //public List<MerchantResponseModel> Items { get; set; }

        [Display(Description = "List of merchants")]
        public List<MerchantResponseModel> Items { get; set; } = new List<MerchantResponseModel>();
    }
}
