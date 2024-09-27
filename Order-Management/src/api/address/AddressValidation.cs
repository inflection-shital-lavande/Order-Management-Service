using FluentValidation;
using order_management.database.dto;

namespace order_management.src.api.address;

public class AddressValidation
{
    public class AddressCreateModelValidator : AbstractValidator<AddressCreateModel>

    {
        public AddressCreateModelValidator()
        {
            RuleFor(address => address.AddressLine1)
               .MinimumLength(2)
               .WithMessage("AddressLine1 must be at least 2 characters long.")
              .MaximumLength(512)
              .WithMessage("AddressLine1 cannot exceed 512 characters.")
              .When(address => !string.IsNullOrEmpty(address.AddressLine1));
             

            RuleFor(address => address.AddressLine2)
                .MinimumLength(2)
                .WithMessage("AddressLine2 must be at least 2 characters long.")
                .MaximumLength(512)
                .WithMessage("AddressLine2 cannot exceed 512 characters.")
                .When(address => !string.IsNullOrEmpty(address.AddressLine2)); 
                //.WithMessage("AddressLine2 should not Null"); ; // Optional field validation

            RuleFor(address => address.City)
                .NotEmpty().WithMessage("City is required.")
                .MinimumLength(2).WithMessage("City must be at least 2 characters long.")
                .MaximumLength(64).WithMessage("City cannot exceed 64 characters.")
                .When(address => !string.IsNullOrEmpty(address.City));
            // .WithMessage("City should not Null"); ;

            RuleFor(address => address.State)
                .MinimumLength(2)
                .WithMessage("State must be at least 2 characters long.")
                .MaximumLength(64)
                .WithMessage("State cannot exceed 64 characters.")
                .When(address => !string.IsNullOrEmpty(address.State));
            // .WithMessage("State should not Null"); ; // Optional field validation

            RuleFor(address => address.Country)
                .MinimumLength(2)
                .WithMessage("Country must be at least 2 characters long.")
                .MaximumLength(32)
                .WithMessage("Country cannot exceed 32 characters.")
                .When(address => !string.IsNullOrEmpty(address.Country));
                //.WithMessage("Country should not Null"); ; // Optional field validation

            RuleFor(address => address.ZipCode)
                .MinimumLength(2)
                .WithMessage("ZipCode must be at least 2 characters long.")
                .MaximumLength(32)
                .WithMessage("ZipCode cannot exceed 32 characters.")
                .When(address => !string.IsNullOrEmpty(address.ZipCode));
                

            

        }
    }
      public class AddressUpdateModelValidator : AbstractValidator<AddressUpdateModel>
      {
          public AddressUpdateModelValidator()
          {

            RuleFor(address => address.AddressLine1)
                
               .NotEmpty()
               .WithMessage("AddressLine1 is required.")
               .MinimumLength(2)
               .WithMessage("AddressLine1 must be at least 2 characters long.")
               .MaximumLength(512)
               .WithMessage("AddressLine1 cannot exceed 512 characters.");

            RuleFor(address => address.AddressLine2)
                .MinimumLength(2)
                .WithMessage("AddressLine2 must be at least 2 characters long.")
                .MaximumLength(512)
                .WithMessage("AddressLine2 cannot exceed 512 characters.")
                .When(address => !string.IsNullOrEmpty(address.AddressLine2)); // Optional field validation

            RuleFor(address => address.City)
                .NotEmpty()
                .WithMessage("City is required.")
                .MinimumLength(2)
               .WithMessage("City must be at least 2 characters long.")
                .MaximumLength(64)
                .WithMessage("City cannot exceed 64 characters.");

            RuleFor(address => address.State)
                .MinimumLength(2)
                .WithMessage("State must be at least 2 characters long.")
                .MaximumLength(64)
                .WithMessage("State cannot exceed 64 characters.")
                .When(address => !string.IsNullOrEmpty(address.State)); // Optional field validation

            RuleFor(address => address.Country)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("Country must be at least 2 characters long.")
                .MaximumLength(32)
                 .WithMessage("Country cannot exceed 32 characters.")
                .When(address => !string.IsNullOrEmpty(address.Country)); // Optional field validation

            RuleFor(address => address.ZipCode)
                .MinimumLength(2)
                .WithMessage("ZipCode must be at least 2 characters long.")
                .MaximumLength(32)
                .WithMessage("ZipCode cannot exceed 32 characters.")
                .When(address => !string.IsNullOrEmpty(address.ZipCode)); // Optional field validation

           
        }
    }

}
