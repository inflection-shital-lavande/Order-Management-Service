using FluentValidation;
using order_management.database.dto;

namespace order_management.src.api.customer;

public class CustomerValidation
{
    public class AddCustomerDTOValidator : AbstractValidator<CustomerCreateModel>

    {
        public AddCustomerDTOValidator()
        {
            //RuleFor(x => x.AddressLine1).NotEmpty().WithMessage("AddressLine1 is required").Length(1, 50);

            RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("AddressLine1 is required")
                    .MaximumLength(128)
                    .WithMessage("AddressLine1 cannot be longer than 128 characters");
           
        }
    }
    public class UpdateCustomerDTOValidator : AbstractValidator<CustomerUpdateModel>
    {
        public UpdateCustomerDTOValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("AddressLine1 is required")
                .MaximumLength(128)
                .WithMessage("AddressLine1 cannot be longer than 128 characters");

        }
    }
}
