using FluentValidation;
using Microsoft.EntityFrameworkCore;
using order_management.database.dto;
using order_management.database.models;

namespace order_management.src.api.customer;

public class CustomerValidation
{
    public class CustomerCreateModelValidator : AbstractValidator<CustomerCreateModel>  
    {
       // private readonly DbContext _context;
        public CustomerCreateModelValidator() {



            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MinimumLength(2)
                .WithMessage("Name must be at least 2 characters long.")
                .MaximumLength(128)
                .WithMessage("Name cannot exceed 128 characters.");

            // Email validation
            RuleFor(c => c.Email)
                 .MinimumLength(5)
                 .WithMessage("Email must be at least 5 characters long.")
                 .MaximumLength(512)
                  .WithMessage("Email cannot exceed 512 characters.")
                  .Must(email => !string.IsNullOrWhiteSpace(email) && email == email.ToLower())
                  .WithMessage("Email should be in lowercase.")
                  .EmailAddress()
                  .WithMessage("Please enter a valid email address in the format: user@gmail.com")
                  .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                  .WithMessage("Email address format is invalid.");

            // PhoneCode validation
            RuleFor(c => c.PhoneCode)
                .NotEmpty()
                .MinimumLength(1)
                .WithMessage("PhoneCode must be at least 1 character long.")
                .MaximumLength(8)
                .WithMessage("PhoneCode cannot exceed 8 characters.")
                .When(customer => !string.IsNullOrEmpty(customer.PhoneCode));

            // Phone validation
            RuleFor(c => c.Phone)
                 .Matches(@"^\d{10}$")
                 .WithMessage("Phone number must be exactly 10 digits.")
                 .MinimumLength(2)
                 .WithMessage("Phone must be at least 2 characters long.")
                .MaximumLength(12)
                .WithMessage("Phone cannot exceed 12 characters.")
                .When(customer => !string.IsNullOrEmpty(customer.Phone));

            // ProfilePicture validation
            RuleFor(c => c.ProfilePicture)
               .MinimumLength(5)
               .WithMessage("ProfilePicture must be at least 5 characters long.")
                .MaximumLength(512)
                .WithMessage("ProfilePicture cannot exceed 512 characters.")
                .When(customer => !string.IsNullOrEmpty(customer.ProfilePicture));

            // TaxNumber validation
            RuleFor(c => c.TaxNumber)
               // .NotEmpty()
                 .MinimumLength(2)
                 .WithMessage("TaxNumber must be at least 2 characters long.")
                .MaximumLength(64)
                .WithMessage("TaxNumber cannot exceed 64 characters.")
                .When(customer => !string.IsNullOrEmpty(customer.TaxNumber));

            // DefaultShippingAddressId validation
            RuleFor(c => c.DefaultShippingAddressId)
                .NotEmpty().NotNull()
                .WithMessage(" Default Shipping Address Id not null.");

            // DefaultBillingAddressId validation
            RuleFor(c => c.DefaultBillingAddressId)
                 .NotEmpty()
                 .When(customer => customer.DefaultBillingAddressId.HasValue)
                 .WithMessage("Billing address Id must be a valid GUID if provided.");

        }
    }
    public class CustomerUpdateModelValidator : AbstractValidator<CustomerUpdateModel>
    {
        public CustomerUpdateModelValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
               .MinimumLength(2)
               .WithMessage("Name must be at least 2 characters long.")
                .MaximumLength(128)
                .WithMessage("Name cannot exceed 128 characters.");


            // Email validation
            RuleFor(c => c.Email)
                .Length(5, 512)
                .WithMessage("Email must be between 5 and 512 characters.")
                .NotEmpty()
                 .WithMessage("Email is required.")
                  .Must(email => !string.IsNullOrWhiteSpace(email) && email == email.ToLower())
                  .WithMessage("Email should be in lowercase.")
                  .EmailAddress()
                  .WithMessage("Please enter a valid email address in the format: user@gmail.com")
                  .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                  .WithMessage("Email address format is invalid.");

            // PhoneCode validation
            RuleFor(c => c.PhoneCode)
                .MinimumLength(1)
                .WithMessage("PhoneCode must be at least 1 character long.")
                .NotEmpty()
                .When(customer => !string.IsNullOrEmpty(customer.PhoneCode));

            // Phone validation
            RuleFor(c => c.Phone)
                .NotEmpty()
                 .MinimumLength(2)
                 .WithMessage("Phone must be at least 2 characters long.")
                .MaximumLength(12)
                .WithMessage("Phone cannot exceed 12 characters.")
                .When(customer => !string.IsNullOrEmpty(customer.Phone));

            // ProfilePicture validation
            RuleFor(c => c.ProfilePicture)
                .MinimumLength(5)
                .WithMessage("ProfilePicture must be at least 5 characters long.")
                .MaximumLength(512)
                .WithMessage("ProfilePicture cannot exceed 512 characters.");
               

            // TaxNumber validation
            RuleFor(c => c.TaxNumber)
                 .MinimumLength(2)
                 .WithMessage("TaxNumber must be at least 2 characters long.")
                .MaximumLength(64)
                .WithMessage("TaxNumber cannot exceed 64 characters.");
               // .When(customer => !string.IsNullOrEmpty(customer.TaxNumber));

        }
    }
}
