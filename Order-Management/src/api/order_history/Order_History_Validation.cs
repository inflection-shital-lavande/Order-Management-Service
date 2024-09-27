using FluentValidation;
using order_management.src.database.dto;
using order_management.src.database.dto.orderHistory;

namespace Order_Management.src.api.order_history
{
    public class Order_History_Validation
    {
        public class OrderHistoryCreateModelValidator : AbstractValidator<OrderHistoryCreateModel>

        {
            public OrderHistoryCreateModelValidator()
            {


                RuleFor(item => item.PreviousStatus)
                    .NotEmpty()
                    .WithMessage("PreviousStatus not empty");
                    
                   

                RuleFor(item => item.Status)
                    .NotEmpty()
                    .WithMessage("Status cannot be empty.");

            }
        }
        public class OrderHistoryUpdateModelValidator : AbstractValidator<OrderHistoryUpdateModel>
        {
            public OrderHistoryUpdateModelValidator()
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
