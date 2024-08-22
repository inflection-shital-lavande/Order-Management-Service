using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using order_management.database.dto;
using order_management.services.interfaces;
using Order_Management.src.services.implementetions;
using Order_Management.src.services.interfaces;

namespace Order_Management.src.api.customerAddress
{
    public class CustomerAddressController
    {
        public CustomerAddressController()
        {

        }


        public async Task<IResult> GetAll(HttpContext httpContext, [FromServices] ICustomerAddress _customerAddressService)
        {
            try
            {
                var addresses = await _customerAddressService.GetAllCustomerAddressesAsync();
                return ApiResponse.Success("Success", "Addresses retrieved successfully", addresses);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving addresses");
            }
        }

        public async Task<IResult> Create(CustomerAddressCreateDTO addr, HttpContext httpContext, [FromServices] ICustomerAddress _customerAddressService)
        {
            try
            {
                if (addr == null)
                {
                    return ApiResponse.BadRequest("Failure", "Invalid address data");
                }

                
                var createdAddress = await _customerAddressService.CreateCustomerAddressAsync(addr);
                return ApiResponse.Success("Success", "customeraddresses created successfully", createdAddress);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while creating the customeraddresses");
            }
        }
    }
}





