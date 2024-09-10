using Microsoft.AspNetCore.Mvc.RazorPages;
using order_management.database.models;
using order_management.domain_types.enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace order_management.database.dto
{
    public class CustomerAddressCreate
    {
        [StringLength(36)]
        [Display(Description = "Id of the customer")]

        public Guid? CustomerId { get; internal set; }

        [StringLength(36)]
        [Display(Description = "Id of the address")]

        public Guid? AddressId { get; internal set; }

        [Display(Description = "Type of address")]

        public AddressTypes AddressType { get;internal set; } = AddressTypes.SHIPPING;
        [Display(Description = "Is this favorite address")]

        public bool? IsFavorite { get;internal set; } = false;
    }
}
