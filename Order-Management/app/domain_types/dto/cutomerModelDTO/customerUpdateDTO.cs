﻿using System.ComponentModel.DataAnnotations;

namespace Order_Management.app.domain_types.dto
{
    public class customerUpdateDTO
    {
        [StringLength(128)]
        public string? Name { get; set; }

        [StringLength(512)]

        public string? Email { get; set; }

        [StringLength(8)]
        public string? PhoneCode { get; set; }

        [StringLength(64)]

        public string? Phone { get; set; }

        [StringLength(512)]
        public string? ProfilePicture { get; set; }

        [StringLength(64)]

        public string? TaxNumber { get; set; }
    }
}
