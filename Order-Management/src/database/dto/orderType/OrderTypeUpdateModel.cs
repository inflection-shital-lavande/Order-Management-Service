using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.orderType
{
    public class OrderTypeUpdateModel
    {
        [StringLength(128)]
        public string? Name { get; set; }
        public string? Description { get; set; }

    }
}
