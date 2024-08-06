using FluentValidation;
using Order_Management.database.dto;

namespace Order_Management.src.api.customer
{
    public class CustomerValidation
    {
        public class AddCustomerDTOValidator : AbstractValidator<CustomerCreateDTO>

        {
            public AddCustomerDTOValidator()
            {
                //RuleFor(x => x.AddressLine1).NotEmpty().WithMessage("AddressLine1 is required").Length(1, 50);

                RuleFor(x => x.Name).NotEmpty().WithMessage("AddressLine1 is required").MaximumLength(128).WithMessage("AddressLine1 cannot be longer than 128 characters");
               
            }
        }
        public class UpdateCustomerDTOValidator : AbstractValidator<CustomerUpdateDTO>
        {
            public UpdateCustomerDTOValidator()
            {

                RuleFor(x => x.Name).NotEmpty().WithMessage("AddressLine1 is required").MaximumLength(128).WithMessage("AddressLine1 cannot be longer than 128 characters");

            }
        }
    }
}
