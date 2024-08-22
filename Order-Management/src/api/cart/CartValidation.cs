using FluentValidation;
using order_management.database.dto;
using Order_Management.src.database.dto.cart;

namespace Order_Management.src.api.cart;

public class CartValidation
{
    public class AddCartDTOValidator : AbstractValidator<CartCreateModel>

    {
        public AddCartDTOValidator()
        {


            /* RuleFor(x => x.AddressLine1)
                    .NotEmpty()
                    .MaximumLength(512)
                    .WithMessage("AddressLine1 cannot be longer than 512 characters");*/


        }
    }
    public class UpdateCartDTOValidator : AbstractValidator<CartUpdateModel>
    {
        public UpdateCartDTOValidator()
        {

            

        }
    }

}
