using Order_Management.app.domain_types.enums;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.app.domain_types.dto
{
    public class customerAddressCreateDTO
    {
        [StringLength(36)]
        public Guid? CustomerId { get; set; }

        [StringLength(36)]
        public Guid? AddressId { get; set; }

        public AddressTypes AddressType { get; set; } = AddressTypes.SHIPPING;

        public bool? IsFavorite { get; set; } = false;
    }
}
