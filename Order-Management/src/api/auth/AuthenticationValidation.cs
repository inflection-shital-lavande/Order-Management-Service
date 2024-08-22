using FluentValidation;
using order_management.auth;

namespace order_management.src.api.auth;

public class AuthenticationValidation
{
    public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(x => x.Name)
                   .NotEmpty()
                   .WithMessage("Name is required")
                   .MaximumLength(50)
                   .WithMessage("Name cannot be longer than 50 characters");

            RuleFor(x => x.Email)
                  .NotEmpty()
                  .WithMessage("Email is required.")
                  .Must(email => !string.IsNullOrWhiteSpace(email) && email == email.ToLower())
                  .WithMessage("Email should be in lowercase.")
                  .EmailAddress()
                  .WithMessage("Please enter a valid email address in the format: user@example.com")
                  .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                  .WithMessage("Email address format is invalid.");
  
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{6,}$")
                .WithMessage("Password must be at least 6 characters long and contain at least one letter and one number");
        }
    }
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Please enter a valid email address in the format: user@example.com");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{6,}$")
                .WithMessage("Password must be at least 6 characters long and contain at least one letter and one number");
        }
    }
}
