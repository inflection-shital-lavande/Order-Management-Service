using FluentValidation;
using Order_Management.database.dto;

namespace Order_Management.src.api.coupons
{
    public class CouponsValidation
    {
        public class AddCouponsDTOValidator : AbstractValidator<CouponCreateDTO>

        {
            public AddCouponsDTOValidator()
            {
                //RuleFor(x => x.AddressLine1).NotEmpty().WithMessage("AddressLine1 is required").Length(1, 50);

                RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(64).WithMessage("Name cannot be longer than 64 characters");
                
            }
        }
        public class UpdateCouponDTOValidator : AbstractValidator<CouponUpdateDTO>
        {
            public UpdateCouponDTOValidator()
            {

                RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(64).WithMessage("Name cannot be longer than 64 characters");
                
            }
        }
    }
}
