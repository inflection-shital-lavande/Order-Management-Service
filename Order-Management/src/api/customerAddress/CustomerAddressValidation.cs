﻿using FluentValidation;
using order_management.database.dto;
using Order_Management.src.database.dto.cart;

namespace Order_Management.src.api.customerAddress
{
    public class CustomerAddressValidation
    {
        public class AddCADTOValidator : AbstractValidator<CustomerAddressCreateDTO>

        {
            public AddCADTOValidator()
            {


                /* RuleFor(x => x.AddressLine1)
                        .NotEmpty()
                        .MaximumLength(512)
                        .WithMessage("AddressLine1 cannot be longer than 512 characters");*/


            }
        }
       
    }
}
