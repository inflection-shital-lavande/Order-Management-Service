using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using order_management.database.dto;
using order_management.database.models;
using order_management.services.implementetions;
using order_management.services.interfaces;
using Order_Management.src.database.dto.cart;
using Order_Management.src.services.implementetions;
using Order_Management.src.services.interfaces;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Order_Management.src.api.cart;


    public class CartController: Controller
    {
    

    [ProducesResponseType(200, Type = typeof(IEnumerable<Cart>))]

        public async Task<IResult> GetAll(HttpContext context, HttpContext httpContext, ICartService _cartService)
        {
            try
            {
                var cart = await _cartService.GetAll();
                return ApiResponse.Success("Success", "Cart retrieved successfully", cart);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving carts");
            }
        }
        public async Task<IResult> GetById(Guid id, HttpContext httpContext, ICartService _cartService)
        {
            try
            {
                var cart = await _cartService.GetById(id);
                return cart == null ? ApiResponse.NotFound("Failure", "Cart not found")
                                       : ApiResponse.Success("Success", "Cart retrieved successfully", cart);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving the cart");
            }
        }


    public async Task<IResult> Create(CartCreateModel cart, HttpContext httpContext, ICartService _cartService, IValidator<CartCreateModel> _createValidator)
    {
        try
        {
            // Check if the cart data is null
            if (cart == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid cart data");
            }

                       // Validate using FluentValidation
            var validationResult = _createValidator.Validate(cart);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            // Optional: Additional data annotation validation
            var validationContext = new ValidationContext(cart);
            var validationResults = new List<ValidationResult>();

            // Validate object using data annotations
            bool isValid = Validator.TryValidateObject(cart, validationContext, validationResults, true);
            if (!isValid)
            {
                return ApiResponse.BadRequest("Failure", validationResults.Select(v => v.ErrorMessage));
            }

            // Proceed with the creation of the cart
            var createdCart = await _cartService.Create(cart);
            return ApiResponse.Success("Success", "Cart created successfully", createdCart);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while creating the cart");
        }
    }



    public async Task<IResult> Update(Guid id, CartUpdateModel cart, HttpContext httpContext, ICartService _cartService, IValidator<CartUpdateModel> _updateValidator)
        {
            try
            {
                if (cart == null)
                {
                    return ApiResponse.BadRequest("Failure", "Invalid cart data");
                }

                var validationResult = _updateValidator.Validate(cart);
                if (!validationResult.IsValid)
                {
                    return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
                }
                var validationContext = new ValidationContext(cart); 
            var vResult = new List<ValidationResult>();

            var isvalid = Validator.TryValidateObject(cart, validationContext, vResult, true);

            if (isvalid)
            {

                var updatedCart = await _cartService.Update(id, cart);
                return updatedCart == null ? ApiResponse.NotFound("Failure", "cart not found")
                                              : ApiResponse.Success("Success", "cart updated successfully", updatedCart);
            }
            return Results.BadRequest(vResult);
        }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while updating the cart");
            }
        }
        public async Task<IResult> Delete(Guid id, HttpContext httpContext, ICartService _cartService)
        {
            try
            {
                var success = await _cartService.Delete(id);
                return success ? ApiResponse.Success("Success", "cart deleted successfully")
                               : ApiResponse.NotFound("Failure", "cart not found");
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while deleting the cart");
            }
        }




     public async Task<IResult> Search(HttpContext httpContext, ICartService _cartService,
                                         [FromQuery] Guid? customerId,
                                         [FromQuery] Guid? productId,
                                         [FromQuery] int? totalItemsCountGreaterThan,
                                         [FromQuery] int? totalItemsCountLessThan,
                                         [FromQuery] float? totalAmountGreaterThan,
                                         [FromQuery] float? totalAmountLessThan,
                                         [FromQuery] DateTime? createdBefore,
                                         [FromQuery] DateTime? createdAfter)
                                        // HttpContext httpContext, ICartService _cartService)
     {
         try
         {
             var filter = new CartSearchFilter
             {
                 CustomerId = customerId,
                 ProductId = productId,
                 TotalItemsCountGreaterThan = totalItemsCountGreaterThan,
                 TotalItemsCountLessThan = totalItemsCountLessThan,
                 TotalAmountGreaterThan = totalAmountGreaterThan,
                 TotalAmountLessThan = totalAmountLessThan,
                 CreatedBefore = createdBefore,
                 CreatedAfter = createdAfter

             };

             var carts = await _cartService.Search(filter);
             return carts.Items.Any()
                 ? ApiResponse.Success("Success", "cart retrieved successfully with filters", carts)
                 : ApiResponse.NotFound("Failure", "No carts found matching the filters");
         }
         catch (Exception ex)
         {
             return ApiResponse.Exception(ex, "Failure", "An error occurred while searching for cart");
         }
     }
}






