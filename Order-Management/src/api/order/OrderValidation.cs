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


            

            RuleFor(order => order.CustomerId)
                .NotEmpty()
                 .NotNull().WithMessage("Customer Id is required.");

            // Validate that AssociatedCartId is a valid GUID if provided
            RuleFor(order => order.AssociatedCartId)
                .NotEmpty()
                .Must(cartId => cartId == null || cartId != Guid.Empty)
                .WithMessage("Associated Cart Id must be a valid GUID.");

            // Validate that Notes have a length between 5 and 1024 characters
            RuleFor(order => order.Notes)
              //  .NotEmpty()
                .Length(5, 1024)
                .WithMessage("Notes must be between 5 and 1024 characters.");
            

        }
    }
    public class UpdateOrderDTOValidator : AbstractValidator<OrderUpdateModel>
    {
        public UpdateOrderDTOValidator()
        {

          
          
            // Validate that OrderDiscount is a non-negative value
            RuleFor(order => order.OrderDiscount)
                .NotEmpty()
                .GreaterThanOrEqualTo(0.0).WithMessage("Order discount must be a non-negative value.");

            // Validate that TipAmount is a non-negative value
            RuleFor(order => order.TipAmount)
                .GreaterThanOrEqualTo(0.0).WithMessage("Tip amount must be a non-negative value.");

            // Validate that Notes have a length between 5 and 1024 characters if provided
            RuleFor(order => order.Notes)
                // .NotEmpty()
                .Length(5, 1024)
                .WithMessage("Notes must be between 5 and 1024 characters.");

        }
    }

}
