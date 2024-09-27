using FluentValidation;
using Order_Management.src.database.dto.order_line_item;
using Order_Management.src.database.dto.orderType;

namespace Order_Management.src.api.orderType;

public class Order_Type_Validation
{
    public class OrderTypeCreateModelValidator : AbstractValidator<OrderTypeCreateModel>

    {
        public OrderTypeCreateModelValidator()
        {


            RuleFor(item => item.Name)
               .NotNull()
               .WithMessage("Tax is required.")
               .MaximumLength(128)
               .WithMessage("name max charater is 128");
               
             

            RuleFor(item => item.Description)
                .NotEmpty().NotNull()
               .WithMessage("Tax is required.");
              

        }
    }
    public class OrderTypeUpdateModelValidator : AbstractValidator<OrderTypeUpdateModel>
    {
        public OrderTypeUpdateModelValidator()
        {


            RuleFor(item => item.Name)
              .NotEmpty()
              .WithMessage("Tax is required.")
              .MaximumLength(128)
              .WithMessage("name max charater is 128");



            RuleFor(item => item.Description)
                .NotEmpty()
               .WithMessage("Tax is required.");

        }
    }
}
