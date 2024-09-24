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


            RuleFor(item => item.Name).NotEmpty().NotNull()
               .NotNull().WithMessage("Tax is required.")
              .WithMessage("Tax cannot be empty.");

            RuleFor(item => item.Description).NotEmpty().NotNull()
               .NotNull().WithMessage("Tax is required.")
               .WithMessage("Tax cannot be empty.");

        }
    }
    public class UpdateOrderTypeDTOValidator : AbstractValidator<OrderTypeUpdateModel>
    {
        public UpdateOrderTypeDTOValidator()
        {



        }
    }
}
