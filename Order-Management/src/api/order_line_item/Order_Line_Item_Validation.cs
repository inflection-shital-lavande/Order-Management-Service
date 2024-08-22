using FluentValidation;
using order_management.database.dto;
using Order_Management.src.database.dto.order_line_item;

namespace Order_Management.src.api.order_line_item;

public class Order_Line_Item_Validation
{
    public class AddOrderLineItemDTOValidator : AbstractValidator<OrderLineItemCreateModel>

    {
        public AddOrderLineItemDTOValidator()
        {


            /* RuleFor(x => x.AddressLine1)
                    .NotEmpty()
                    .MaximumLength(512)
                    .WithMessage("AddressLine1 cannot be longer than 512 characters");*/

          

        }
    }
    public class UpdateOrderLineItemDTOValidator : AbstractValidator<OrderLineItemUpdateModel>
    {
        public UpdateOrderLineItemDTOValidator()
        {

            

        }
    }

}