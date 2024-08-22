using FluentValidation;
using order_management.database.dto;
using Order_Management.src.database.dto.merchant;

namespace Order_Management.src.api.merchant;

public class MerchantValidation

{
    public class AddMerchantDTOValidator : AbstractValidator<MerchantCreateModel>

    {
        public AddMerchantDTOValidator()
        {


            /* RuleFor(x => x.AddressLine1)
                    .NotEmpty()
                    .MaximumLength(512)
                    .WithMessage("AddressLine1 cannot be longer than 512 characters");*/

            

        }
    }
    public class UpdateMerchantDTOValidator : AbstractValidator<MerchantUpdateModel>
    {
        public UpdateMerchantDTOValidator()
        {

          

        }
    }

}


