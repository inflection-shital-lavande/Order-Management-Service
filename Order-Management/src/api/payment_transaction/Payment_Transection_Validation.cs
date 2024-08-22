using FluentValidation;
using Order_Management.src.database.dto.orderType;
using Order_Management.src.database.dto.payment_transaction;

namespace Order_Management.src.api.payment_transaction;

public class Payment_Transection_Validation
{
    public class AddPaymentTransactionDTOValidator : AbstractValidator<PaymentTransactionCreateModel>

    {
        public AddPaymentTransactionDTOValidator()
        {


            /* RuleFor(x => x.AddressLine1)
                    .NotEmpty()
                    .MaximumLength(512)
                    .WithMessage("AddressLine1 cannot be longer than 512 characters");*/



        }
    }
   
}
