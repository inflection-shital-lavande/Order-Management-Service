using FluentValidation;
using order_management.database.dto;

namespace order_management.src.api.address;

public class AddressValidation
{
    public class AddAddressDTOValidator : AbstractValidator<AddressCreateModel>

    {
        public AddAddressDTOValidator()
        {
            RuleFor(address => address.AddressLine1)
               .MinimumLength(2).WithMessage("AddressLine1 must be at least 2 characters long.")
              .MaximumLength(512).WithMessage("AddressLine1 cannot exceed 512 characters.")
              .When(address => !string.IsNullOrEmpty(address.AddressLine1));
             

            RuleFor(address => address.AddressLine2)
                .MinimumLength(2).WithMessage("AddressLine2 must be at least 2 characters long.")
                .MaximumLength(512).WithMessage("AddressLine2 cannot exceed 512 characters.")
                .When(address => !string.IsNullOrEmpty(address.AddressLine2)); 
                //.WithMessage("AddressLine2 should not Null"); ; // Optional field validation

            RuleFor(address => address.City)
                //.NotEmpty().WithMessage("City is required.")
                .MinimumLength(2).WithMessage("City must be at least 2 characters long.")
                .MaximumLength(64).WithMessage("City cannot exceed 64 characters.")
                .When(address => !string.IsNullOrEmpty(address.City));
            // .WithMessage("City should not Null"); ;

            RuleFor(address => address.State)
                .MinimumLength(2).WithMessage("State must be at least 2 characters long.")
                .MaximumLength(64).WithMessage("State cannot exceed 64 characters.")
                .When(address => !string.IsNullOrEmpty(address.State));
            // .WithMessage("State should not Null"); ; // Optional field validation

            RuleFor(address => address.Country)
                .MinimumLength(2).WithMessage("Country must be at least 2 characters long.")
                .MaximumLength(32).WithMessage("Country cannot exceed 32 characters.")
                .When(address => !string.IsNullOrEmpty(address.Country));
                //.WithMessage("Country should not Null"); ; // Optional field validation

            RuleFor(address => address.ZipCode)
                .MinimumLength(2).WithMessage("ZipCode must be at least 2 characters long.")
                .MaximumLength(32).WithMessage("ZipCode cannot exceed 32 characters.")
                .When(address => !string.IsNullOrEmpty(address.ZipCode));
                //.WithMessage("Zipcode should not Null");

            /*  RuleFor(x => x.AddressLine1)
                     .NotEmpty();
                      //.MaximumLength(512)
                      //.WithMessage("AddressLine1 cannot be longer than 512 characters");

              RuleFor(x => x.City)
                     .NotEmpty();
                    // .MaximumLength(64)
                     //.WithMessage("City cannot be longer than 64 characters");

              RuleFor(x => x.State)
                     .NotEmpty()//.WithMessage("State is required")
                     .MaximumLength(64)
                     .WithMessage("State cannot be longer than 64 characters");

              RuleFor(x => x.Country)
                     .NotEmpty();
                    // .WithMessage("Country is required")
                    // .MaximumLength(32)
                    //.WithMessage("Country cannot be longer than 32 characters");

              RuleFor(x => x.ZipCode)
                     .NotEmpty();
                    // .WithMessage("ZipCode is required")
                    // .MaximumLength(32)
                    // .WithMessage("ZipCode cannot be longer than 32 characters");*/

        }
    }
      public class UpdateAddressDTOValidator : AbstractValidator<AddressUpdateModel>
      {
          public UpdateAddressDTOValidator()
          {

            RuleFor(address => address.AddressLine1)
                
              // .NotEmpty().WithMessage("AddressLine1 is required.")
               .MinimumLength(2).WithMessage("AddressLine1 must be at least 2 characters long.")
               .MaximumLength(512).WithMessage("AddressLine1 cannot exceed 512 characters.");

            RuleFor(address => address.AddressLine2)
                .MinimumLength(2).WithMessage("AddressLine2 must be at least 2 characters long.")
                .MaximumLength(512).WithMessage("AddressLine2 cannot exceed 512 characters.")
                .When(address => !string.IsNullOrEmpty(address.AddressLine2)); // Optional field validation

            RuleFor(address => address.City)
               // .NotEmpty().WithMessage("City is required.")
                .MinimumLength(2).WithMessage("City must be at least 2 characters long.")
                .MaximumLength(64).WithMessage("City cannot exceed 64 characters.");

            RuleFor(address => address.State)
                .MinimumLength(2).WithMessage("State must be at least 2 characters long.")
                .MaximumLength(64).WithMessage("State cannot exceed 64 characters.")
                .When(address => !string.IsNullOrEmpty(address.State)); // Optional field validation

            RuleFor(address => address.Country)
                .MinimumLength(2).WithMessage("Country must be at least 2 characters long.")
                .MaximumLength(32).WithMessage("Country cannot exceed 32 characters.")
                .When(address => !string.IsNullOrEmpty(address.Country)); // Optional field validation

            RuleFor(address => address.ZipCode)
                .MinimumLength(2).WithMessage("ZipCode must be at least 2 characters long.")
                .MaximumLength(32).WithMessage("ZipCode cannot exceed 32 characters.")
                .When(address => !string.IsNullOrEmpty(address.ZipCode)); // Optional field validation

           
            /* RuleFor(x => x.AddressLine1)
                    .NotEmpty().WithMessage("AddressLine1 is not empty");
             //.NotNull().WithMessage("address shiould not null");
             // .MaximumLength(512)
             //.WithMessage("AddressLine1 cannot be longer than 512 characters");

             RuleFor(x => x.City)
                    .NotEmpty();
             // .WithMessage("City is required");
             // .MaximumLength(64)
             //.WithMessage("City cannot be longer than 64 characters");

             RuleFor(x => x.State)
                    .NotEmpty();
             // .WithMessage("State is required");
             // .MaximumLength(64)
             //.WithMessage("State cannot be longer than 64 characters");

             RuleFor(x => x.Country)
                    .NotEmpty();
              .WithMessage("Country is required")
              .MaximumLength(32)
              .WithMessage("Country cannot be longer than 32 characters");

            RuleFor(x => x.ZipCode)
                  .NotEmpty();
                 // .WithMessage("ZipCode is required")
                  //.MaximumLength(32)
                  //.WithMessage("ZipCode cannot be longer than 32 characters");*/

        }
    }

}
