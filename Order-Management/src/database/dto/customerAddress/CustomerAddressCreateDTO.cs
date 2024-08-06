using Order_Management.domain_types.enums;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.database.dto
{
    public class CustomerAddressCreateDTO
    {
        [StringLength(36)]
        public Guid? CustomerId { get; set; }

        [StringLength(36)]
        public Guid? AddressId { get; set; }

        public AddressTypes AddressType { get; set; } = AddressTypes.SHIPPING;

        public bool? IsFavorite { get; set; } = false;
    }
}
