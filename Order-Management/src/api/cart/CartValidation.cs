using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using order_management.database.dto;
using Order_Management.src.database.dto.cart;

namespace Order_Management.src.api.cart;

public class CartValidation
{
    public class CartCreateModelValidator : AbstractValidator<CartCreateModel>

    {
        public CartCreateModelValidator()
        {

            RuleFor(cart => cart.CustomerId)
           .NotNull().WithMessage("CustomerId is required")
           .NotEmpty().WithMessage("CustomerId cannot be an empty GUID");
         //  .Must(IsValidGuid).WithMessage("CustomerId must be a valid GUID");

            // Validate TotalItemsCount (Optional, must be a positive number or zero)
            RuleFor(x => x.TotalItemsCount)
               .NotEmpty()
              .GreaterThan(0).When(x => x.TotalItemsCount.HasValue)
              .WithMessage("TotalItemsCount cannot be negative.");
              
            // Validate TotalAmount (Optional, must be a positive number or zero)
            RuleFor(x => x.TotalAmount)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("TotalAmount must be Greter then 0.");

            // Validate CartToOrderTimestamp (Optional, must be a valid past or current date if provided)
            RuleFor(x => x.CartToOrderTimestamp)
                .Must(date => !date.HasValue || date.Value <= DateTime.Now)
                .WithMessage("CartToOrderTimestamp must be in the past or present.");

        
                   }

       
    }
    public class CartUpdateModelValidator : AbstractValidator<CartUpdateModel>
    {
        public CartUpdateModelValidator()
        {

            RuleFor(x => x.TotalItemsCount)
              .NotEmpty()
              .WithMessage("Total items count is required.")
              .GreaterThan(0)
              .WithMessage("Total items count must be greater than 0.");

            RuleFor(x => x.TotalTax)
                .GreaterThanOrEqualTo(0).When(x => x.TotalTax.HasValue)
                .WithMessage("Total tax cannot be negative.");

            RuleFor(x => x.TotalDiscount)
                .GreaterThanOrEqualTo(0).When(x => x.TotalDiscount.HasValue)
                .WithMessage("Total discount cannot be negative.");

            RuleFor(x => x.TotalAmount)
                .NotEmpty()
                .WithMessage("Total amount is not null.")
                .GreaterThan(0)
                .WithMessage("Total amount must be greater than 0.");

        }
    }

}
