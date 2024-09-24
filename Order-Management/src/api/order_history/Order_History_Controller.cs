using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using order_management.database.dto;
using order_management.database.models;
using order_management.domain_types.enums;
using order_management.services.interfaces;
using order_management.src.database.dto;
using order_management.src.database.dto.orderHistory;
using order_management.src.services.implementetions;
using order_management.src.services.interfaces;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.api.order_history;


public class Order_History_Controller
{
    public Order_History_Controller()
    {

    }

    [ProducesResponseType(200, Type = typeof(IEnumerable<OrderHistory>))]

    public async Task<IResult> GetAll(HttpContext httpContext, IOrderHistoryService _orderHistoryService)
    {
        try
        {
            var orderHistory = await _orderHistoryService.GetAll();
            return ApiResponse.Success("Success", "orderHistory retrieved successfully", orderHistory);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving orderHistory");
        }
    }
    public async Task<IResult> GetById(Guid id, HttpContext httpContext, IOrderHistoryService _orderHistoryService)
    {
        try
        {
            var orderHistory = await _orderHistoryService.GetById(id);
            return orderHistory == null ? ApiResponse.NotFound("Failure", "orderHistory not found")
                                   : ApiResponse.Success("Success", "orderHistory retrieved successfully", orderHistory);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving the orderHistory");
        }
    }
    public async Task<IResult> Create(OrderHistoryCreateModel orderHistory, HttpContext httpContext, IOrderHistoryService _orderHistoryService, IValidator<OrderHistoryCreateModel> _createValidator)
    {
        try
        {
            if (orderHistory == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid orderHistory data");
            }

            var validationResult = _createValidator.Validate(orderHistory);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }

         /*   var validationContext = new ValidationContext(orderHistory);
            var vResult = new List<ValidationResult>();

            var isvalid = Validator.TryValidateObject(orderHistory, validationContext, vResult, true);

            if (isvalid)
            {*/
                var createdOrderHistory = await _orderHistoryService.Create(orderHistory);
                return ApiResponse.Success("Success", "orderHistory created successfully", createdOrderHistory);
          //  }
          // return Results.BadRequest(vResult);


            ////
           
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while creating the orderHistory");
        }
    }
    public async Task<IResult> Update(Guid id, OrderHistoryUpdateModel orderHistory, HttpContext httpContext, IOrderHistoryService _orderHistoryService, IValidator<OrderHistoryUpdateModel> _updateValidator)
    {
        try
        {
            if (orderHistory == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid orderHistory data");
            }

            var validationResult = _updateValidator.Validate(orderHistory);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }

          /*  var validationContext = new ValidationContext(orderHistory);
            var vResult = new List<ValidationResult>();

            var isvalid = Validator.TryValidateObject(orderHistory, validationContext, vResult, true);

            if (isvalid)
            {*/
                var updatedOrderHistory = await _orderHistoryService.Update(id, orderHistory);
                return updatedOrderHistory == null ? ApiResponse.NotFound("Failure", "orderHistory not found")
                                              : ApiResponse.Success("Success", "orderHistory updated successfully", updatedOrderHistory);
            //}
          //  return Results.BadRequest(vResult);

           
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while updating the orderHistory");
        }
    }
    public async Task<IResult> Delete(Guid id, HttpContext httpContext, IOrderHistoryService _orderHistoryService)
    {
        try
        {
            var success = await _orderHistoryService.Delete(id);
            return success ? ApiResponse.Success("Success", "Address deleted successfully")
                           : ApiResponse.NotFound("Failure", "Address not found");
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while deleting the address");
        }
    }

    public async Task<IResult> Search([FromQuery] Guid? orderId,
                                      [FromQuery] OrderStatusTypes? previousStatus,
                                      [FromQuery] OrderStatusTypes? status,
                                      [FromQuery] Guid? updatedByUserId,
                                      [FromQuery] DateTime? timestamp,

                                       HttpContext httpContext, IOrderHistoryService _orderHistoryService)
    {
        try
        {
            var filter = new OrderHistorySearchFilterModel
            {
                OrderId = orderId,
                PreviousStatus = previousStatus ?? OrderStatusTypes.DRAFT,
                Status = status ?? OrderStatusTypes.DRAFT,
                UpdatedByUserId = updatedByUserId,
                Timestamp = timestamp
            };

            var orderHistory = await _orderHistoryService.Search(filter);
            return orderHistory.Items.Any()
                ? ApiResponse.Success("Success", "orderHistory retrieved successfully with filters", orderHistory)
                : ApiResponse.NotFound("Failure", "No orderHistory found matching the filters");
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while searching for orderHistory");
        }
    }
}