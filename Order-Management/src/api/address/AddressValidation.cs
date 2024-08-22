using FluentValidation;
using order_management.database.dto;

namespace order_management.src.api.address;

public class AddressValidation
{
    public class AddAddressDTOValidator : AbstractValidator<AddressCreateModel>

    {
        public AddAddressDTOValidator()
        {
            

           /* RuleFor(x => x.AddressLine1)
                   .NotEmpty()
                   .MaximumLength(512)
                   .WithMessage("AddressLine1 cannot be longer than 512 characters");*/

            RuleFor(x => x.City)
                   .NotEmpty()
                   .MaximumLength(64)
                   .WithMessage("City cannot be longer than 64 characters");

            RuleFor(x => x.State)
                   .NotEmpty().WithMessage("State is required")
                   .MaximumLength(64)
                   .WithMessage("State cannot be longer than 64 characters");

            RuleFor(x => x.Country)
                   .NotEmpty()
                   .WithMessage("Country is required")
                   .MaximumLength(32)
                   .WithMessage("Country cannot be longer than 32 characters");

            RuleFor(x => x.ZipCode)
                   .NotEmpty()
                   .WithMessage("ZipCode is required")
                   .MaximumLength(32)
                   .WithMessage("ZipCode cannot be longer than 32 characters");

        }
    }
      public class UpdateAddressDTOValidator : AbstractValidator<AddressUpdateModel>
      {
          public UpdateAddressDTOValidator()
          {
              
            RuleFor(x => x.AddressLine1)
                   .NotEmpty()
                   .WithMessage("AddressLine1 is required")
                   .MaximumLength(512)
                   .WithMessage("AddressLine1 cannot be longer than 512 characters");

            RuleFor(x => x.City)
                   .NotEmpty()
                   .WithMessage("City is required")
                   .MaximumLength(64)
                   .WithMessage("City cannot be longer than 64 characters");

            RuleFor(x => x.State)
                   .NotEmpty()
                   .WithMessage("State is required")
                   .MaximumLength(64)
                   .WithMessage("State cannot be longer than 64 characters");

            RuleFor(x => x.Country)
                   .NotEmpty()
                   .WithMessage("Country is required")
                   .MaximumLength(32)
                   .WithMessage("Country cannot be longer than 32 characters");

            RuleFor(x => x.ZipCode)
                  .NotEmpty()
                  .WithMessage("ZipCode is required")
                  .MaximumLength(32)
                  .WithMessage("ZipCode cannot be longer than 32 characters");

        }
    }

}
