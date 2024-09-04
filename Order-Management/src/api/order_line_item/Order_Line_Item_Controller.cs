using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using order_management.database.dto;
using order_management.database.models;
using order_management.services.implementetions;
using order_management.services.interfaces;
using Order_Management.src.database.dto.order_line_item;
using Order_Management.src.services.interfaces;

namespace Order_Management.src.api.order_line_item;


    public class Order_Line_Item_Controller
    {
        public Order_Line_Item_Controller()
        {

        }

        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderLineItem>))]

        public async Task<IResult> GetAll(HttpContext httpContext, IOrderLineItem _orderLineItemService)
        {
            try
            {
                var orderLineItem = await _orderLineItemService.GetAll();
                return ApiResponse.Success("Success", "orderLineItem retrieved successfully", orderLineItem);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving orderLineItem");
            }
        }
        public async Task<IResult> GetById(Guid id, HttpContext httpContext, IOrderLineItem _orderLineItemService)
        {
            try
            {
                var orderLineItem = await _orderLineItemService.GetById(id);
                return orderLineItem == null ? ApiResponse.NotFound("Failure", "orderLineItem not found")
                                       : ApiResponse.Success("Success", "Address retrieved successfully", orderLineItem);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving the orderLineItem");
            }
        }
        public async Task<IResult> Create(OrderLineItemCreateModel orderLineItem, HttpContext httpContext, IOrderLineItem _orderLineItemService, IValidator<OrderLineItemCreateModel> _createValidator)
        {
            try
            {
                if (orderLineItem == null)
                {
                    return ApiResponse.BadRequest("Failure", "Invalid orderLineItem data");
                }

                var validationResult = _createValidator.Validate(orderLineItem);
                if (!validationResult.IsValid)
                {
                    return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
                }

                var createdOrderLineItem = await _orderLineItemService.Create(orderLineItem);
                return ApiResponse.Success("Success", "orderLineItem created successfully", createdOrderLineItem);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while creating the orderLineItem");
            }
        }
        public async Task<IResult> Update(Guid id, OrderLineItemUpdateModel orderLineItem, HttpContext httpContext, IOrderLineItem _orderLineItemService, IValidator<OrderLineItemUpdateModel> _updateValidator)
        {
            try
            {
                if (orderLineItem == null)
                {
                    return ApiResponse.BadRequest("Failure", "Invalid address data");
                }

                var validationResult = _updateValidator.Validate(orderLineItem);
                if (!validationResult.IsValid)
                {
                    return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
                }

                var updatedOrderLineItem = await _orderLineItemService.Update(id, orderLineItem);
                return updatedOrderLineItem == null ? ApiResponse.NotFound("Failure", "orderLineItem not found")
                                              : ApiResponse.Success("Success", "orderLineItem updated successfully", updatedOrderLineItem);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while updating the orderLineItem");
            }
        }
        public async Task<IResult> Delete(Guid id, HttpContext httpContext, IOrderLineItem _orderLineItemService)
        {
            try
            {
                var success = await _orderLineItemService.Delete(id);
                return success ? ApiResponse.Success("Success", "orderLineItem deleted successfully")
                               : ApiResponse.NotFound("Failure", "orderLineItem not found");
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while deleting the orderLineItem");
            }
        }

        public async Task<IResult> Search([FromQuery] string? name,
                                          [FromQuery] Guid? catalogId,
                                          [FromQuery] Guid? discountSchemeId,
                                          [FromQuery] double? itemSubTotal,
                                          [FromQuery] Guid? orderId,
                                          [FromQuery] Guid? cartId,
                                          [FromQuery] DateTime? createdBefore,
                                          [FromQuery] DateTime? createdAfter,

                                          HttpContext httpContext, IOrderLineItem _orderLineItemService)
        {
            try
            {
                var filter = new OrderLineItemSearchFilter
                {
                    Name = name,
                    CatalogId = catalogId,
                    DiscountSchemeId = discountSchemeId,
                    ItemSubTotal = itemSubTotal,
                    OrderId = orderId,
                    CartId = cartId,
                    CreatedBefore = createdBefore,
                    CreatedAfter = createdAfter
                };

                var orderLineItem = await _orderLineItemService.Search(filter);
                return orderLineItem.Items.Any()
                    ? ApiResponse.Success("Success", "orderLineItem retrieved successfully with filters", orderLineItem)
                    : ApiResponse.NotFound("Failure", "No orderLineItem found matching the filters");
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while searching for orderLineItem");
            }
        }
    }