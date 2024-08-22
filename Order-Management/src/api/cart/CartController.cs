using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using order_management.database.dto;
using order_management.database.models;
using order_management.services.interfaces;
using Order_Management.src.database.dto.cart;
using Order_Management.src.services.interfaces;

namespace Order_Management.src.api.cart;


    public class CartController
    {
        public CartController()
        {

        }

        [ProducesResponseType(200, Type = typeof(IEnumerable<Address>))]

        public async Task<IResult> GetAll(HttpContext httpContext, ICartService _cartService)
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

                var createdAddress = await _cartService.Create(cart);
                return ApiResponse.Success("Success", "Address created successfully", createdAddress);
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

                var updatedCart = await _cartService.Update(id, cart);
                return updatedCart == null ? ApiResponse.NotFound("Failure", "cart not found")
                                              : ApiResponse.Success("Success", "cart updated successfully", updatedCart);
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

        public async Task<IResult> Search(Guid? CustomerId,


                                                  HttpContext httpContext, ICartService _cartService)
        {
            try
            {
                var filter = new CartSearchFilter
                {
                    CustomerId = CustomerId

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