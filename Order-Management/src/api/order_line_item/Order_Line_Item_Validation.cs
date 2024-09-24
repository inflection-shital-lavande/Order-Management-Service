using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using order_management.database.dto;
using Order_Management.src.database.dto.order_line_item;

namespace Order_Management.src.api.order_line_item;

public class Order_Line_Item_Validation
{
    public class AddOrderLineItemDTOValidator : AbstractValidator<OrderLineItemCreateModel>

    {
        public AddOrderLineItemDTOValidator()
        {
            RuleFor(item => item.Name)
               .NotEmpty().WithMessage("Name is required.")
               .Length(3, 255).WithMessage("Name must be between 3 and 255 characters long.");

            // Validate that CatalogId, if provided, is a valid non-empty GUID
            RuleFor(item => item.CatalogId)
                .Must(catalogId => catalogId == null || catalogId != Guid.Empty)
                .WithMessage("CatalogId must be a valid GUID.");

            // Validate that Quantity is required and is at least 1
            RuleFor(item => item.Quantity)
                .NotNull().WithMessage("Quantity is required.")
                .GreaterThanOrEqualTo(1).WithMessage("Quantity must be at least 1.");

            // Validate that UnitPrice is required and greater than zero
            RuleFor(item => item.UnitPrice)
                .NotEmpty()
                .NotNull().WithMessage("UnitPrice is required.")
                .GreaterThan(0.0).WithMessage("UnitPrice must be greater than zero.");

            // Validate that Discount is non-negative
            RuleFor(item => item.Discount)
                .GreaterThanOrEqualTo(0.0).WithMessage("Discount cannot be negative.");

            // Validate that DiscountSchemeId, if provided, is a valid non-empty GUID
            RuleFor(item => item.DiscountSchemeId)
                .Must(discountSchemeId => discountSchemeId == null || discountSchemeId != Guid.Empty)
                .WithMessage("DiscountSchemeId must be a valid GUID.");

            // Validate that Tax is required and non-negative
            RuleFor(item => item.Tax)
                .NotNull().WithMessage("Tax is required.")
                .GreaterThanOrEqualTo(0.0).WithMessage("Tax cannot be negative.");

            // Validate that ItemSubTotal is required and greater than zero
            RuleFor(item => item.ItemSubTotal)
                .NotNull().WithMessage("ItemSubTotal is required.")
                .GreaterThan(0.0).WithMessage("ItemSubTotal must be greater than zero.");

            // Validate that OrderId is required and is a valid non-empty GUID
            RuleFor(item => item.OrderId)
                .NotNull().WithMessage("OrderId is required.")
                .NotEqual(Guid.Empty).WithMessage("OrderId must be a valid GUID.");

            // Validate that CartId is required and is a valid non-empty GUID
            RuleFor(item => item.CartId)
                .NotNull().WithMessage("CartId is required.")
                .NotEqual(Guid.Empty).WithMessage("CartId must be a valid GUID.");


        }
    }
    public class UpdateOrderLineItemDTOValidator : AbstractValidator<OrderLineItemUpdateModel>
    {
        public UpdateOrderLineItemDTOValidator()
        {

            RuleFor(item => item.Name)
               .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");

            // Validate that Quantity is at least 1 if provided
            RuleFor(item => item.Quantity)
                .GreaterThanOrEqualTo(1).When(item => item.Quantity.HasValue)
                .WithMessage("Quantity must be at least 1.");

            // Validate that UnitPrice is greater than zero if provided
            RuleFor(item => item.UnitPrice)
                .GreaterThan(0.0).When(item => item.UnitPrice.HasValue)
                .WithMessage("UnitPrice must be greater than zero.");

            // Validate that Discount is non-negative if provided
            RuleFor(item => item.Discount)
                .GreaterThanOrEqualTo(0.0).When(item => item.Discount.HasValue)
                .WithMessage("Discount cannot be negative.");


            // Validate that Tax is non-negative if provided
            RuleFor(item => item.Tax)
                .GreaterThanOrEqualTo(0.0).When(item => item.Tax.HasValue)
                .WithMessage("Tax cannot be negative.");

            // Validate that ItemSubTotal is greater than zero if provided
            RuleFor(item => item.ItemSubTotal)
                .GreaterThan(0.0).When(item => item.ItemSubTotal.HasValue)
                .WithMessage("ItemSubTotal must be greater than zero.");

            // Validate that OrderId is required
            RuleFor(item => item.OrderId)
                .NotEmpty().WithMessage("OrderId is required.");

            // Validate that CartId is required
            RuleFor(item => item.CartId)
                .NotEmpty().WithMessage("CartId is required.");

        }
    }

}