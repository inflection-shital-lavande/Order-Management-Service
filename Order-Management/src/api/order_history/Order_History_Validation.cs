using FluentValidation;
using order_management.src.database.dto;
using order_management.src.database.dto.orderHistory;

namespace Order_Management.src.api.order_history
{
    public class Order_History_Validation
    {
        public class AddOrderHistoryDTOValidator : AbstractValidator<OrderHistoryCreateModel>

        {
            public AddOrderHistoryDTOValidator()
            {


                RuleFor(item => item.PreviousStatus)
                    .NotEmpty()
                    .WithMessage("PreviousStatus cannot be empty.");

                RuleFor(item => item.Status)
                    .NotEmpty()
               .WithMessage("Status cannot be empty.");

            }
        }
        public class UpdateOrderHistoryDTOValidator : AbstractValidator<OrderHistoryUpdateModel>
        {
            public UpdateOrderHistoryDTOValidator()
            {

                RuleFor(item => item.PreviousStatus)
                    .NotEmpty()
                    .WithMessage("PreviousStatus cannot be empty.");

                RuleFor(item => item.Status)
                .NotEmpty()
               .WithMessage("Status cannot be empty.");

            }
        }

    }
}
