using FluentValidation;
using Order_Management.src.database.dto.order_line_item;
using Order_Management.src.database.dto.orderType;

namespace Order_Management.src.api.orderType;

public class Order_Type_Validation
{
    public class AddOrdeTypeDTOValidator : AbstractValidator<OrderTypeCreateModel>

    {
        public AddOrdeTypeDTOValidator()
        {


            /* RuleFor(x => x.AddressLine1)
                    .NotEmpty()
                    .MaximumLength(512)
                    .WithMessage("AddressLine1 cannot be longer than 512 characters");*/



        }
    }
    public class UpdateOrderTypeDTOValidator : AbstractValidator<OrderTypeUpdateModel>
    {
        public UpdateOrderTypeDTOValidator()
        {



        }
    }
}
