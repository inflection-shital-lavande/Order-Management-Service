using FluentValidation;
using order_management.database.dto;
using Order_Management.src.database.dto.merchant;

namespace Order_Management.src.api.merchant;

public class MerchantValidation

{
    public class MerchantCreateModelValidator : AbstractValidator<MerchantCreateModel>

    {
        public MerchantCreateModelValidator()
        {

            RuleFor(x => x.ReferenceId)
                       .NotNull()
                       .WithMessage("ReferenceId is required.")
                       .NotEmpty()
                       .WithMessage("ReferenceId should be a valid UUID.");

            RuleFor(x => x.Name)
                .MinimumLength(5)
                .WithMessage("Name must be at least 5 characters long.")
                .MaximumLength(512)
                .WithMessage("Name can be a maximum of 512 characters.");

            RuleFor(x => x.Email)
                .Must(email => !string.IsNullOrWhiteSpace(email) && email == email.ToLower())
                  .WithMessage("Email should be in lowercase.")
                  .WithMessage("Please enter a valid email address in the format: user@gmail.com")
                  .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                  .WithMessage("Email address format is invalid.")
                  .Length(5, 512)
                  .WithMessage("Email must be at least min 5 and max 512 long.");
                

                
            RuleFor(x => x.Phone)
                 .NotEmpty()
                 .Matches(@"^\d{10}$")
                 .WithMessage("Phone number must be exactly 10 digits.")
                 .MinimumLength(2)
                 .WithMessage("Phone number must be at least 2 characters long.")
                .MaximumLength(12)
                .WithMessage("Phone number can be a maximum of 12 characters.");
               // .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.Logo)
                .MinimumLength(5)
                .WithMessage("Logo URL must be at least 5 characters long.")
                .MaximumLength(512)
                .WithMessage("Logo URL can be a maximum of 512 characters.")
                .When(x => !string.IsNullOrEmpty(x.Logo));

            RuleFor(x => x.WebsiteUrl)
                .MinimumLength(5)
                .WithMessage("Website URL must be at least 5 characters long.")
                .MaximumLength(512)
                .WithMessage("Website URL can be a maximum of 512 characters.");


            RuleFor(x => x.TaxNumber)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("Tax number/code must be at least 2 characters long.")
                .MaximumLength(64)
                .WithMessage("Tax number/code can be a maximum of 64 characters.");
                

            RuleFor(x => x.GSTNumber)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("GST number/code must be at least 2 characters long.")
                .MaximumLength(64)
                .WithMessage("GST number/code can be a maximum of 64 characters.")
                .When(x => !string.IsNullOrEmpty(x.GSTNumber));

            RuleFor(x => x.AddressId)
                .NotEmpty() 
                .NotNull()
                .WithMessage("AddressId is required.");

        }
    }
    public class MerchantUpdateModelValidator : AbstractValidator<MerchantUpdateModel>
    {
        public MerchantUpdateModelValidator()
        {

            RuleFor(x => x.Name)
              .NotEmpty()
             .MinimumLength(5)
             .WithMessage("Name must be at least 5 characters long.")
             .MaximumLength(512)
             .WithMessage("Name can be a maximum of 512 characters.");
            

            RuleFor(x => x.Email)
                  .NotEmpty()
                  .Must(email => !string.IsNullOrWhiteSpace(email) && email == email.ToLower())
                  .WithMessage("Email should be in lowercase.")
                  .EmailAddress()
                  .WithMessage("Please enter a valid email address in the format: user@example.com")
                  .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                  .WithMessage("Email address format is invalid.")
                  .Length(5, 512).WithMessage("Email must be at least min 5 and max 512 long.");


            RuleFor(x => x.Phone)
                 .Matches(@"^\d{10}$")
                 .WithMessage("Phone number must be exactly 10 digits.")
                 .NotEmpty()
                 .MinimumLength(2)
                 .WithMessage("Phone number must be at least 2 characters long.");
               

            RuleFor(x => x.Logo)
                .NotEmpty()
                .MinimumLength(5)
                .WithMessage("Logo URL must be at least 5 characters long.")
                .MaximumLength(512)
                .WithMessage("Logo URL can be a maximum of 512 characters.");
               // .When(x => !string.IsNullOrEmpty(x.Logo));

            RuleFor(x => x.TaxNumber)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("Tax number/code must be at least 2 characters long.")
                .MaximumLength(64)
                .WithMessage("Tax number/code can be a maximum of 64 characters.");
            // .When(x => !string.IsNullOrEmpty(x.TaxNumber));

            RuleFor(x => x.GSTNumber)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("GST number/code must be at least 2 characters long.")
                .MaximumLength(64)
                .WithMessage("GST number/code can be a maximum of 64 characters.");
            // .When(x => !string.IsNullOrEmpty(x.GSTNumber));

            RuleFor(x => x.AddressId)
                .NotEmpty()
                .NotNull().WithMessage("AddressId is required.");
               

        }
    }

}


