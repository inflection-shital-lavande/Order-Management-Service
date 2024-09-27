using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using order_management.database.dto;
using order_management.database.models;
using order_management.services.interfaces;
using order_management.src.database.dto.orderHistory;
using order_management.src.services.interfaces;
using Order_Management.src.database.dto.orderType;
using Order_Management.src.services.implementetions;
using Order_Management.src.services.interfaces;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.src.api.orderType
{
    public class Order_Type_Controller
    {

        public Order_Type_Controller()
        {

        }

        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderType>))]

        public async Task<IResult> GetById(Guid id, HttpContext httpContext, IOrderTypeService service)
        {
            try
            {
                var orderType = await service.GetById(id);
                return orderType == null ? ApiResponse.NotFound("Failure", "OrderType not found")
                                       : ApiResponse.Success("Success", "OrderType retrieved successfully", orderType);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving the address");
            }
        }

        public async Task<IResult> GetAll(HttpContext httpContext, IOrderTypeService service)
        {
            try
            {
                var orderTypes = await service.GetAll();
                return ApiResponse.Success("Success", "OrderTypes retrieved successfully", orderTypes);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving orderTypes");
            }
        }

        public async Task<IResult> Create(OrderTypeCreateModel orderTypes, HttpContext httpContext, IOrderTypeService service, IValidator<OrderTypeCreateModel> _createValidator)
        {
            try
            {
                if (orderTypes == null)
                {
                    return ApiResponse.BadRequest("Failure", "Invalid orderTypes data");
                }

                var validationResult = _createValidator.Validate(orderTypes);
                if (!validationResult.IsValid)
                {
                    return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
                }

                var validationContext = new ValidationContext(orderTypes);
                var vResult = new List<ValidationResult>();

                var isvalid = Validator.TryValidateObject(orderTypes, validationContext, vResult, true);

                if (isvalid)
                {
                    var createdOrderTypes = await service.Create(orderTypes);
                    return ApiResponse.Success("Success", "OrderTypes created successfully", createdOrderTypes);
                }
                return Results.BadRequest(vResult);

                
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while creating the orderTypes");
            }
        }
        public async Task<IResult> Update(Guid id, OrderTypeUpdateModel orderTypes, HttpContext httpContext, IOrderTypeService service, IValidator<OrderTypeUpdateModel> _updateValidator)
        {
            try
            {
                if (orderTypes == null)
                {
                    return ApiResponse.BadRequest("Failure", "Invalid orderTypes data");
                }

                var validationResult = _updateValidator.Validate(orderTypes);
                if (!validationResult.IsValid)
                {
                    return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
                }

                var validationContext = new ValidationContext(orderTypes);
                var vResult = new List<ValidationResult>();

                var isvalid = Validator.TryValidateObject(orderTypes, validationContext, vResult, true);

                if (isvalid)
                {
                    var updatedOrderTypes = await service.Update(id, orderTypes);
                    return updatedOrderTypes == null ? ApiResponse.NotFound("Failure", "OrderTypes not found")
                                                  : ApiResponse.Success("Success", "OrderTypes updated successfully", updatedOrderTypes);
                }
                return Results.BadRequest(vResult);

               
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while updating the orderTypes");
            }
        }
        public async Task<IResult> Delete(Guid id, HttpContext httpContext, IOrderTypeService service)
        {
            try
            {
                var success = await service.Delete(id);
                return success ? ApiResponse.Success("Success", "OrderType deleted successfully")
                               : ApiResponse.NotFound("Failure", "OrderType not found");
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while deleting the OrderType");
            }
        }
        public async Task<IResult> Search(string? Name,string? Description,

                                             HttpContext httpContext, IOrderTypeService _orderTypeService)
        {
            try
            {
                var filter = new OrderTypeSearchFilter
                {
                    Name= Name,
                    Description= Description
                };

                var orderType = await _orderTypeService.Search(filter);
                return orderType.Items.Any()
                    ? ApiResponse.Success("Success", "orderHistory retrieved successfully with filters", orderType)
                    : ApiResponse.NotFound("Failure", "No orderHistory found matching the filters");
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while searching for orderHistory");
            }
        }
    }
    }

