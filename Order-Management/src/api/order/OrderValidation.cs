using FluentValidation;
using order_management.database.dto;
using order_management.src.database.dto;

namespace Order_Management.src.api.order;

public class OrderValidation
{
    public class AddOrderDTOValidator : AbstractValidator<OrderCreateModel>

    {
        public AddOrderDTOValidator()
        {


            /* RuleFor(x => x.AddressLine1)
                    .NotEmpty()
                    .MaximumLength(512)
                    .WithMessage("AddressLine1 cannot be longer than 512 characters");*/


        }
    }
    public class UpdateOrderDTOValidator : AbstractValidator<OrderUpdateModel>
    {
        public UpdateOrderDTOValidator()
        {

          

        }
    }

}
