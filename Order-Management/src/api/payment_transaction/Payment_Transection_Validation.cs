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


            // Validate InvoiceNumber
            RuleFor(payment => payment.InvoiceNumber).NotNull()
                .NotEmpty().WithMessage("InvoiceNumber is required.");

            // Validate BankTransactionId (if present)
            RuleFor(payment => payment.BankTransactionId).NotEmpty()
               // .Must(IsGuidValid).When(payment => payment.BankTransactionId.HasValue)
                .WithMessage("BankTransactionId must be a valid GUID.");

            // Validate PaymentGatewayTransactionId (if present)
            RuleFor(payment => payment.PaymentGatewayTransactionId).NotEmpty()
               // .Must(IsGuidValid).When(payment => payment.PaymentGatewayTransactionId.HasValue)
                .WithMessage("PaymentGatewayTransactionId must be a valid GUID.");

            // Validate PaymentMode (optional)
            RuleFor(payment => payment.PaymentMode)
                .MaximumLength(50).WithMessage("PaymentMode cannot exceed 50 characters.");

            RuleFor(payment => payment.PaymentStatus).NotEmpty()
               // .GreaterThan(0).When(payment => payment.PaymentCurrency.HasValue)
                .WithMessage("PaymentCurrency must be greater than zero.");

            // Validate PaymentAmount
            RuleFor(payment => payment.PaymentAmount).NotNull().NotEmpty()
                .GreaterThan(0).WithMessage("PaymentAmount cannot be negative.");

            // Validate PaymentCurrency (optional)
            RuleFor(payment => payment.PaymentCurrency)
                .GreaterThan(0).When(payment => payment.PaymentCurrency.HasValue)
                .WithMessage("PaymentCurrency must be greater than zero.");

            RuleFor(payment => payment.PaymentResponse).NotNull().NotEmpty()
               // .GreaterThan(0).When(payment => payment.PaymentCurrency.HasValue)
                .WithMessage("PaymentCurrency must be greater than zero.");

            RuleFor(payment => payment.PaymentResponseCode).NotNull().NotEmpty()
                //.GreaterThan(0).When(payment => payment.PaymentCurrency.HasValue)
                .WithMessage("PaymentCurrency must be greater than zero.");


            // Validate InitiatedBy
            RuleFor(payment => payment.InitiatedBy)
                .NotEmpty().WithMessage("InitiatedBy is required.")
                .MaximumLength(100).WithMessage("InitiatedBy cannot exceed 100 characters.");

        }
    }
   
}
