using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using order_management.database.dto;
using order_management.database.models;
using order_management.domain_types.enums;
using order_management.services.interfaces;
using order_management.src.database.dto;
using order_management.src.services.interfaces;

namespace Order_Management.src.api.order;

    public class OrderController
    {
        public OrderController()
        {

        }

        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]

        public async Task<IResult> GetAll(HttpContext httpContext, IOrderService _orderService)
        {
            try
            {
                var order = await _orderService.GetAll();
                return ApiResponse.Success("Success", "order retrieved successfully", order);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving order");
            }
        }
        public async Task<IResult> GetById(Guid id, HttpContext httpContext, IAddressService _orderService)
        {
            try
            {
                var order = await _orderService.GetById(id);
                return order == null ? ApiResponse.NotFound("Failure", "order not found")
                                       : ApiResponse.Success("Success", "order retrieved successfully", order);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving the order");
            }
        }
        public async Task<IResult> Create(OrderCreateModel order, HttpContext httpContext, IOrderService _orderService, IValidator<OrderCreateModel> _createValidator)
        {
            try
            {
                if (order == null)
                {
                    return ApiResponse.BadRequest("Failure", "Invalid order data");
                }

                var validationResult = _createValidator.Validate(order);
                if (!validationResult.IsValid)
                {
                    return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
                }

                var createdOrder = await _orderService.Create(order);
                return ApiResponse.Success("Success", "Order created successfully", createdOrder);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while creating the Order");
            }
        }
        public async Task<IResult> Update(Guid id, OrderUpdateModel order, HttpContext httpContext, IOrderService _orderService, IValidator<OrderUpdateModel> _updateValidator)
        {
            try
            {
                if (order == null)
                {
                    return ApiResponse.BadRequest("Failure", "Invalid order data");
                }

                var validationResult = _updateValidator.Validate(order);
                if (!validationResult.IsValid)
                {
                    return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
                }

                var updatedOrder = await _orderService.Update(id, order);
                return updatedOrder == null ? ApiResponse.NotFound("Failure", "order not found")
                                              : ApiResponse.Success("Success", "order updated successfully", updatedOrder);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while updating the order");
            }
        }
        public async Task<IResult> Delete(Guid id, HttpContext httpContext, IOrderService _orderService)
        {
            try
            {
                var success = await _orderService.Delete(id);
                return success ? ApiResponse.Success("Success", "order deleted successfully")
                               : ApiResponse.NotFound("Failure", "order not found");
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while deleting the order");
            }
        }

        public async Task<IResult> Search([FromQuery] Guid? customerId,
                                          [FromQuery] Guid? associatedCartId,
                                          [FromQuery] Guid? couponId,
                                          [FromQuery] int? totalItemsCountGreaterThan,
                                          [FromQuery] int? totalItemsCountLessThan,
                                          [FromQuery] float? orderDiscountGreaterThan,
                                          [FromQuery] float? orderDiscountLessThan,
                                          [FromQuery] bool? tipApplicable,
                                          [FromQuery] float? totalAmountGreaterThan,
                                          [FromQuery] float? totalAmountLessThan,
                                          [FromQuery] Guid? orderLineItemProductId,
                                          [FromQuery] OrderStatusTypes orderStatus,
                                          [FromQuery] Guid? orderTypeId,
                                          [FromQuery] DateTime? createdBefore,
                                          [FromQuery] DateTime? createdAfter,
                                          [FromQuery] int? pastMonths,

                                                  HttpContext httpContext, IOrderService _orderService)
        {
            try
            {
                var filter = new OrderSearchFilterModel
                {

                    CustomerId = customerId,
                    AssociatedCartId = associatedCartId,
                    CouponId = couponId,
                    TotalItemsCountGreaterThan = totalItemsCountGreaterThan,
                    TotalItemsCountLessThan = totalItemsCountLessThan,
                    TipApplicable = tipApplicable,
                    OrderDiscountGreaterThan = orderDiscountGreaterThan,
                    OrderDiscountLessThan = orderDiscountLessThan,
                    TotalAmountGreaterThan = totalAmountGreaterThan,
                    TotalAmountLessThan = totalAmountLessThan,
                    OrderLineItemProductId = orderLineItemProductId,
                    OrderStatus = orderStatus,
                    OrderTypeId = orderTypeId,
                    CreatedBefore = createdBefore,
                    CreatedAfter = createdAfter,
                    PastMonths = pastMonths,

                };

                var order = await _orderService.Search(filter);
                return order.Items.Any()
                    ? ApiResponse.Success("Success", "order retrieved successfully with filters", order)
                    : ApiResponse.NotFound("Failure", "No order found matching the filters");
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while searching for order");
            }
        }
    }
