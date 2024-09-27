using FluentValidation;
using order_management.database.dto;

namespace order_management.src.api.coupons;

public class CouponsValidation
{
    public class CouponsCreateModelValidator : AbstractValidator<CouponCreateModel>

    {
        public CouponsCreateModelValidator()
        {
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("name at least 2 character long")
               .MaximumLength(64)
               .WithMessage("Coupon name must be between 64 characters.");
               //.When(x => !string.IsNullOrEmpty(x.Name));
           


            // Validate Description length
            RuleFor(c => c.Description)
                .MinimumLength(2)
                .WithMessage("Description at least 5 character long")
                .MaximumLength(1024)
                .WithMessage("Description cannot exceed 1024 characters.");

            // Validate CouponCode length
            RuleFor(c => c.CouponCode)
                
               .MinimumLength( 2)
               .WithMessage("Coupon code must be  2  characters.")
                .MaximumLength(64)
                .WithMessage("CouponCode cannot exceed 64 characters.");
           

            // Validate CouponType length
            RuleFor(c => c.CouponType)
                .MinimumLength(2)
                .WithMessage("Coupontype code must be 2  characters.")
                 .MaximumLength(64)
                 .WithMessage("CouponType cannot exceed 64 characters.");

            // Validate Discount
            RuleFor(c => c.Discount)
                .NotEmpty()
                .WithMessage("Discount is required")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Discount must be greater than or equal to 0.");
           

            // Validate DiscountPercentage
            RuleFor(c => c.DiscountPercentage)
                .NotEmpty()
                .WithMessage("DiscountPercentage is required")
                .InclusiveBetween(0, 100)
                .WithMessage("DiscountPercentage must be between 0 and 100.");

            // Validate DiscountMaxAmount
            RuleFor(c => c.DiscountMaxAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Discount max amount must be greater than or equal to 0.");
               
            // Validate MaxUsage
            RuleFor(c => c.MaxUsage)
                .InclusiveBetween(0,10000)
                 .WithMessage("MaxUsagePerUser must be between 0 and 10000");

            // Validate MaxUsagePerUser
            RuleFor(c => c.MaxUsagePerUser)
                .InclusiveBetween(0, 10)
                .WithMessage("MaxUsagePerUser must be between 0 and 10.");

            // Validate MaxUsagePerOrder
            RuleFor(c => c.MaxUsagePerOrder)
                .InclusiveBetween(0, 5)
                .WithMessage("MaxUsagePerOrder must be between 0 and 5.");
                

            // Validate MinOrderAmount
            RuleFor(c => c.MinOrderAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MinOrderAmount must be greater than or equal to 0.");


            // Validate StartDate and EndDate (StartDate should be before EndDate)
           
                 RuleFor(x => x.StartDate)
                 .LessThanOrEqualTo(x => x.EndDate)
                 .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
                 .WithMessage("Start date must be earlier than or equal to the end date.");
          
        }
    }
    public class CouponUpdateModelValidator : AbstractValidator<CouponUpdateModel>
    {
        public CouponUpdateModelValidator()
        {

            

             RuleFor(x => x.Name)
               // .NotEmpty()
                //.WithMessage("Name is required.")
               .MinimumLength(2)
                .WithMessage("name at least 2 character long")
               .MaximumLength(64)
               .WithMessage("Coupon name must be between 64 characters.");

            RuleFor(x => x.Description)
                 .Length(2, 1024)
                 .WithMessage("Description must be between 2 and 1024 characters.")
                 .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.CouponCode)
                .NotEmpty()
                .WithMessage("coupon code should not empty")
                 .MinimumLength(2)
                 .WithMessage("Coupon code must be between 2 characters.")
                 .MaximumLength( 64)
                 .WithMessage("Coupon code must be between 64 characters.")
                 .When(x => !string.IsNullOrEmpty(x.CouponCode));

             RuleFor(x => x.CouponType)
                .NotEmpty()
                .WithMessage("coupon type code should not empty")
                 .MinimumLength(2)
                 .WithMessage("Coupon type code must be between 2 characters.")
                 .MaximumLength(64)
                 .WithMessage("Coupon type must be between 64 characters.")
                 .When(x => !string.IsNullOrEmpty(x.CouponType));

             RuleFor(x => x.Discount)
                   .GreaterThanOrEqualTo(0)
                   .WithMessage("Discount must be a non-negative value.");
            

            RuleFor(x => x.DiscountPercentage)
                .NotEmpty() 
                 .InclusiveBetween(0, 100)
                 .WithMessage("Discount percentage must be between 0 and 100.");

             RuleFor(x => x.DiscountMaxAmount)
                 .GreaterThanOrEqualTo(0)
                 .WithMessage("Discount max amount must be a non-negative value.");

             RuleFor(x => x.StartDate)
                .NotEmpty()
                 .LessThanOrEqualTo(x => x.EndDate)
                 .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
                 .WithMessage("Start date must be earlier than or equal to the end date.");

             RuleFor(x => x.MaxUsage)
                 .InclusiveBetween(0, 10000)
                 .WithMessage("Max usage must be between 0 and 10000.");

             RuleFor(x => x.MaxUsagePerUser)
                .NotNull()
                .NotEmpty() 
                .WithMessage("Max usage per user must be NotEmpty");

             RuleFor(x => x.MaxUsagePerOrder)
                .NotNull()
                  .NotEmpty()
                .WithMessage("Max usage per Order must be NotEmpty");

            RuleFor(x => x.MinOrderAmount)
                 .GreaterThanOrEqualTo(0)
                 .WithMessage("Minimum order amount must be a non-negative value.");

             RuleFor(x => x.IsActive)
                 .NotNull()
                 .WithMessage("IsActive field is required.");

             RuleFor(x => x.IsDeleted)
                 .NotNull()
                 .WithMessage("IsDeleted field is required.");
        }
    }
}
