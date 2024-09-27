using FluentValidation;
using Order_Management.src.database.dto.orderType;
using Order_Management.src.database.dto.payment_transaction;

namespace Order_Management.src.api.payment_transaction;

public class Payment_Transection_Validation
{
    public class PaymentTransactionCreateModelValidator : AbstractValidator<PaymentTransactionCreateModel>

    {
        public PaymentTransactionCreateModelValidator()
        {

            RuleFor(x => x.DisplayCode)
                .MaximumLength(36)
                .WithMessage("length 36");
           //.NotEmpty()
           //.WithMessage("Display code is required.");

            RuleFor(x => x.InvoiceNumber)
                .MaximumLength(64)
                .WithMessage("length 64");
            //.NotEmpty()
            //.WithMessage("Invoice number is required.");

            RuleFor(x => x.PaymentAmount)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(0.0f)
                .WithMessage("Payment amount must be a positive value.");

            RuleFor(x => x.InitiatedBy)
                .NotEmpty()
                .WithMessage("Initiated by is required.");

            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .NotNull()
                .WithMessage("not null")
                .WithMessage("Customer Id is required.");

            RuleFor(x => x.OrderId)
                .NotEmpty()
                .WithMessage("Order Id is required.");

            RuleFor(x => x.PaymentGatewayTransactionId)
                .NotEmpty();
               // .MaximumLength(512)
                //.WithMessage("Payment Gateway Transaction Id cannot exceed 512 characters.");

            RuleFor(x => x.PaymentMode)
                .NotEmpty()
                .MaximumLength(256)
                .WithMessage("Payment Mode cannot exceed 256 characters.");

            RuleFor(x => x.PaymentCurrency)
               .NotEmpty();

            RuleFor(x => x.PaymentResponse)
                .NotEmpty()
                .NotNull()
                .WithMessage("not null")
                .MaximumLength(1024)
                .WithMessage("Payment Response cannot exceed 1024 characters.");

            RuleFor(x => x.PaymentResponseCode)
                .NotEmpty()
                .NotNull()
                .WithMessage("not null")
                .MaximumLength(256)
                .WithMessage("Payment Response Code cannot exceed 256 characters.");

        }
    }
   
}
