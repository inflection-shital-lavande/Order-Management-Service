using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.database.dto.orderType
{
    public class OrderTypeCreateModel
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
