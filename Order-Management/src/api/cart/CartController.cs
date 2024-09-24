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

namespace Order_Management.src.api.cart;


    public class CartController: Controller
    {
    /*private readonly ICartService _cartService;
    private readonly IValidator<CartCreateModel> _createValidator;
    private readonly IValidator<CartUpdateModel> _updateValidator;
    public CartController(ICartService cartService,
                             IValidator<CartCreateModel> createValidator,
                             IValidator<CartUpdateModel> updateValidator)
    {
        _cartService = cartService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }*/

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
                if (cart == null)
                {
                    return ApiResponse.BadRequest("Failure", "Invalid cart data");
                }

                var validationResult = _createValidator.Validate(cart);
                if (!validationResult.IsValid)
                {
                    return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
                }
            var validationContext = new ValidationContext(cart);
            var vResult = new List<ValidationResult>();

            var isvalid = Validator.TryValidateObject(cart, validationContext, vResult, true);

            if (isvalid)
            {

                var createdAddress = await _cartService.Create(cart);
                 return ApiResponse.Success("Success", "cart created successfully", createdAddress);
            }
            return Results.BadRequest(vResult);

            //var createdAddress = await _cartService.Create(cart);
              //  return ApiResponse.Success("Success", "cart created successfully", createdAddress);
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






/*public async Task<IResult> Search(HttpContext context)
{
    try
    {
        // Extract query parameters from HttpContext.Request
        var query = context.Request.Query;

        var filter = new CartSearchFilter
        {
            CustomerId = query.ContainsKey("customerId") ? Guid.Parse(query["customerId"]) : (Guid?)null,
            ProductId = query.ContainsKey("productId") ? Guid.Parse(query["productId"]) : (Guid?)null,
            TotalItemsCountGreaterThan = query.ContainsKey("totalItemsCountGreaterThan") ? int.Parse(query["totalItemsCountGreaterThan"]) : (int?)null,
            TotalItemsCountLessThan = query.ContainsKey("totalItemsCountLessThan") ? int.Parse(query["totalItemsCountLessThan"]) : (int?)null,
            TotalAmountGreaterThan = query.ContainsKey("totalAmountGreaterThan") ? float.Parse(query["totalAmountGreaterThan"]) : (float?)null,
            TotalAmountLessThan = query.ContainsKey("totalAmountLessThan") ? float.Parse(query["totalAmountLessThan"]) : (float?)null,
            CreatedBefore = query.ContainsKey("createdBefore") ? DateTime.Parse(query["createdBefore"]) : (DateTime?)null,
            CreatedAfter = query.ContainsKey("createdAfter") ? DateTime.Parse(query["createdAfter"]) : (DateTime?)null
        };

        var carts = await _cartService.Search(filter);
        return carts.Items.Any()
            ? ApiResponse.Success("Success", "Carts retrieved successfully with filters", carts)
            : ApiResponse.NotFound("Failure", "No carts found matching the filters");
    }
    catch (Exception ex)
    {
        return ApiResponse.Exception(ex, "Failure", "An error occurred while searching for carts");
    }
}*/