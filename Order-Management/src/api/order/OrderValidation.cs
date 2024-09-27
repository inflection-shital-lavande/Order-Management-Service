using FluentValidation;
using order_management.database.dto;
using order_management.src.database.dto;

namespace Order_Management.src.api.order;

public class OrderValidation
{
    public class OrderCreateModelValidator : AbstractValidator<OrderCreateModel>

    {
        public OrderCreateModelValidator()
        {


            

            RuleFor(order => order.CustomerId)
                     .NotNull()
                     .WithMessage("Customer Id is required.");

            // Validate that AssociatedCartId is a valid GUID if provided
           /* RuleFor(order => order.OrderTypeId)
                .NotEmpty()
                .Must(Id => Id == null || Id != Guid.Empty)
                .WithMessage("OrderTypeId Cart Id must be a valid GUID.");*/

            // Validate that Notes have a length between 5 and 1024 characters
            RuleFor(order => order.Notes)
                .Length(5, 1024)
                .WithMessage("Notes must be between 5 and 1024 characters.");
            

        }
    }
    public class OrderUpdateModelValidator : AbstractValidator<OrderUpdateModel>
    {
        public OrderUpdateModelValidator()
        {

          
          
            // Validate that OrderDiscount is a non-negative value
            RuleFor(order => order.OrderDiscount)
                .NotEmpty()
                .GreaterThanOrEqualTo(0.0)
                .WithMessage("Order discount must be a non-negative value.");

            // Validate that TipAmount is a non-negative value
            RuleFor(order => order.TipAmount)
                .NotEmpty()
                .GreaterThanOrEqualTo(0.0)
                .WithMessage("Tip amount must be a non-negative value.");

            // Validate that Notes have a length between 5 and 1024 characters if provided
            RuleFor(order => order.Notes)
                 .NotEmpty()
                .Length(5, 1024)
                .WithMessage("Notes must be between 5 and 1024 characters.");

        }
    }

}
