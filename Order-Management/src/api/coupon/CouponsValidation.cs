using FluentValidation;
using order_management.database.dto;

namespace order_management.src.api.coupons;

public class CouponsValidation
{
    public class AddCouponsDTOValidator : AbstractValidator<CouponCreateModel>

    {
        public AddCouponsDTOValidator()
        {
            //RuleFor(x => x.AddressLine1).NotEmpty().WithMessage("AddressLine1 is required").Length(1, 50);

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(64).WithMessage("Name cannot be longer than 64 characters");
            
        }
    }
    public class UpdateCouponDTOValidator : AbstractValidator<CouponUpdateModel>
    {
        public UpdateCouponDTOValidator()
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(64).WithMessage("Name cannot be longer than 64 characters");
            
        }
    }
}
