using AutoMapper;
using Azure.Core;
using FluentValidation;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using order_management.common;

using Order_Management.database.dto;
using Order_Management.services.implementetions;
using Order_Management.services.interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace Order_Management.api;

public static class AddressController
{
    public static async Task<IResult> GetAllAddresses(IAddressService addressService)
    {
        try
        {
            var addresses = await addressService.GetAll();
            return ApiResponse.Success("Success","Addresses retrieved successfully", addresses);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex,"failure"," not found"); 
        }
    }

    public static async Task<IResult> GetAddressById(IAddressService addressService, Guid id)
    {
        try
        {
            var address = await addressService.GetAddressById(id);
            return address == null ? ApiResponse.NotFound("Failure","Address not found") : ApiResponse.Success("Success", "Address retrieved successfully", address);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex,"failure","not found"); 
        }
    }

    
    public static async Task<IResult> AddAddress( IAddressService addressService,IValidator<AddressCreateDTO> validator, [FromBody] AddressCreateDTO addrDTO)
    {
        try
        {
            if (addrDTO == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid address data");
            }


            var validationResult = validator.Validate(addrDTO);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var validationContext = new ValidationContext(addrDTO);
            var vResult = new List<ValidationResult>();

            var isvalid = Validator.TryValidateObject(addrDTO, validationContext, vResult, true);

            if (isvalid)
            {
                var createdAddress = await addressService.CreateAddress(addrDTO);
                return ApiResponse.Success("success", "Addresses Create successfully ", addrDTO);
            }
            return Results.BadRequest(vResult);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure","not found"); 
        }
    }


    public static async Task<IResult> UpdateAddress(IAddressService addressService, IValidator<AddressUpdateDTO> validator, Guid id, [FromBody] AddressUpdateDTO addrDTO)
    {
        try
        {
            if (addrDTO == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid address data");
            }

            var validationResult = validator.Validate(addrDTO);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }
            var validationContext = new ValidationContext(addrDTO);
            var vResult = new List<ValidationResult>();

            var isvalid = Validator.TryValidateObject(addrDTO, validationContext, vResult, true);

            if (isvalid)
            {
                var updatedAddress = await addressService.UpdateAddress(id, addrDTO);
                return updatedAddress == null ? ApiResponse.NotFound("Failure", "Address not found") : ApiResponse.Success("Success", "Address updated successfully", addrDTO);
            }
            return Results.BadRequest(vResult);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure","not found"); // Include stack trace for debugging
        }
    }

    
    public static async Task<IResult> DeleteAddress(IAddressService addressService, Guid id)
    {
        

        try
        {
            return await addressService.DeleteAddress(id);

        }
        catch (Exception ex)
        {
            
            return ApiResponse.Exception(ex,"Failure","An error occurred while processing your request.");
        }
    }



/*    public static async Task<IResult> GetCustomerByAddress(IAddressService addressService, Guid id,IMapper mapper)
    {


        try
        {
            var GetCustomerByAddress = addressService.GetCustomerByAddress(id);

            if (GetCustomerByAddress == null || !GetCustomerByAddress.Any())
            {
                return Results.NotFound();
            }

            var pokemonDtos = mapper.Map<List<CustomerResponseDTO>>(GetCustomerByAddress);
            return Results.Ok(pokemonDtos);
        }
        catch (Exception ex)
        {

            return ApiResponse.Exception(ex, "Failure", "An error occurred while processing your request.");
        }
    }*/



public static async Task<IResult> SearchAddresses(IAddressService addressService,
                                                                  [FromQuery] string? AddressLine1,
                                                                  [FromQuery] string? AddressLine2,
                                                                  [FromQuery] string? City,
                                                                  [FromQuery] string? State,
                                                                  [FromQuery] string? Country,
                                                                  [FromQuery] string? ZipCode)
    {
        try
        {
           
                var filterDTO = new AddressSearchFilterDTO
                {
                    AddressLine1 = AddressLine1,
                    AddressLine2 = AddressLine2,
                    City = City,
                    State = State,
                    Country = Country,
                    ZipCode = ZipCode
                };
                var addresses = await addressService.SearchAddress(filterDTO);
            return ApiResponse.Success("Success", "Addresses retrieved successfully with filters", filterDTO);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure","not found"); // Include stack trace for debugging
        }

    }
   
}





/* public static async Task<IResult> DeleteAddress(IAddressService addressService, Guid id)
{
 try
 {
     // Call the DeleteAddressAsync method
     var response = await addressService.DeleteAddressAsync(id);

     // Check if the response indicates failure (e.g., null or failure status)
     if (response == null )
     {
         // Return a NotFound result with a failure message
         return Results.NotFound(ApiResponse.NotFound("Address not found", "Failure"));
     }

     // Return a success result with a success message
     return Results.Ok(ApiResponse.Success("Address deleted successfully", "Success"));
 }
 catch (Exception ex)
 {
     // Return an error result with exception details
     return Results.Problem(ApiResponse.Exception(ex, "Failure", "Exception occurred").ToString());
 }
}
*/


/*  public static async Task<IResult> DeleteAddress(IAddressService addressService, Guid id)
     {
           try
           {
               var deletedAddress = await addressService.DeleteAddressAsync(id);

               return deletedAddress == null ? ApiResponse.NotFound("Failure", "Address not found") : ApiResponse.Success("Success", "Address deleted successfully");
           }
           catch (Exception ex)
           {
               return ApiResponse.Exception( ex,"Failure","not found"); 
           }
       }
   */