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


                /* RuleFor(x => x.AddressLine1)
                        .NotEmpty()
                        .MaximumLength(512)
                        .WithMessage("AddressLine1 cannot be longer than 512 characters");*/


            }
        }
        public class UpdateOrderHistoryDTOValidator : AbstractValidator<OrderHistoryUpdateModel>
        {
            public UpdateOrderHistoryDTOValidator()
            {



            }
        }

    }
}
