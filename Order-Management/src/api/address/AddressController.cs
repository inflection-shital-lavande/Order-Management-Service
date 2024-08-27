
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using order_management.database.dto;
using order_management.database.models;
using order_management.services.implementetions;
using order_management.services.interfaces;

namespace order_management.api;

public class AddressController
{  
    public AddressController()
    {

    }

    [ProducesResponseType(200, Type = typeof(IEnumerable<Address>))]

    public async Task<IResult> GetAll(HttpContext httpContext, IAddressService _addressService)
    {
        try
        {
            var addresses = await _addressService.GetAll();
            return ApiResponse.Success("Success", "Addresses retrieved successfully", addresses);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving addresses");
        }
    }

    


    public async Task<IResult> GetById(Guid id, HttpContext httpContext, IAddressService _addressService)
    {
        try
        {
            var address = await _addressService.GetById(id);
            return address == null ? ApiResponse.NotFound("Failure", "Address not found")
                                   : ApiResponse.Success("Success", "Address retrieved successfully", address);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving the address");
        }
    }
    public async Task<IResult> Create(AddressCreateModel addr, HttpContext httpContext, IAddressService _addressService, IValidator<AddressCreateModel> _createValidator)
    {
        try
        {
            if (addr == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid address data");
            }

            var validationResult = _createValidator.Validate(addr);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var createdAddress = await _addressService.Create(addr);
            return ApiResponse.Success("Success", "Address created successfully", createdAddress);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while creating the address");
        }
    }
    public async Task<IResult> Update(Guid id, AddressUpdateModel addr, HttpContext httpContext, IAddressService _addressService, IValidator<AddressUpdateModel> _updateValidator)
    {
        try
        {
            if (addr == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid address data");
            }

            var validationResult = _updateValidator.Validate(addr);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var updatedAddress = await _addressService.Update(id, addr);
            return updatedAddress == null ? ApiResponse.NotFound("Failure", "Address not found")
                                          : ApiResponse.Success("Success", "Address updated successfully", updatedAddress);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while updating the address");
        }
    }
    public async Task<IResult> Delete(Guid id, HttpContext httpContext, IAddressService _addressService)
    {
        try
        {
            var success = await _addressService.Delete(id);
            return success ? ApiResponse.Success("Success", "Address deleted successfully")
                           : ApiResponse.NotFound("Failure", "Address not found");
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while deleting the address");
        }
    }
  
    public async Task<IResult> Search(string? AddressLine1,
                                              string? AddressLine2,
                                              string? City,
                                              string? State,
                                              string? Country,
                                              string? ZipCode,
                                              HttpContext httpContext, IAddressService _addressService)
    {
        try
        {
            var filter = new AddressSearchFilterModel
            {
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                City = City,
                State = State,
                Country = Country,
                ZipCode = ZipCode
            };

            var addresses = await _addressService.Search(filter);
            return addresses.Items.Any()
                ?ApiResponse.Success("Success", "Addresses retrieved successfully with filters", addresses)
                : ApiResponse.NotFound("Failure", "No addresses found matching the filters");
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while searching for addresses");
        }
    }

    


    
}

/*

 using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using order_management.database;
using order_management.database.dto;
using order_management.services.implementetions;
using order_management.services.interfaces;

namespace order_management.api;



public class AddressController
{
    private readonly IAddressService _addressService;
    private readonly IValidator<AddressCreateModel> _createValidator;
    private readonly IValidator<AddressUpdateModel> _updateValidator;


    public AddressController(IAddressService addressService, IValidator<AddressCreateModel> createValidator, IValidator<AddressUpdateModel> updateValidator)
    {
        _addressService = addressService;
        _createValidator= createValidator;  
        _updateValidator= updateValidator;
    }


   public async Task<IResult> GetAll()
    {
        try
        {
            var addresses = await _addressService.GetAll();
            return ApiResponse.Success("Success", "Addresses retrieved successfully", addresses);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving addresses");
        }
    }

    public async Task<IResult> GetById(Guid id, HttpContext httpContext)
    {
        try
        {
            var address = await _addressService.GetById(id);
            return address == null ? ApiResponse.NotFound("Failure", "Address not found")
                                   : ApiResponse.Success("Success", "Address retrieved successfully", address);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving the address");
        }
    }


    public async Task<IResult> Create(AddressCreateModel addr, HttpContext httpContext)
    {
        try
        {
            if (addr == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid address data");
            }

            var validationResult = _createValidator.Validate(addr);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var createdAddress = await _addressService.Create(addr);
            return ApiResponse.Success("Success", "Address created successfully", createdAddress);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while creating the address");
        }
    }


    public async Task<IResult> Update(Guid id, AddressUpdateModel addr, HttpContext httpContext)
    {
        try
        {
            if (addr == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid address data");
            }

            var validationResult = _updateValidator.Validate(addr);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var updatedAddress = await _addressService.Update(id, addr);
            return updatedAddress == null ? ApiResponse.NotFound("Failure", "Address not found")
                                          : ApiResponse.Success("Success", "Address updated successfully", updatedAddress);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while updating the address");
        }
    }

    public async Task<IResult> Delete(Guid id, HttpContext httpContext)
    {
        try
        {
            var success = await _addressService.Delete(id);
            return success ? ApiResponse.Success("Success", "Address deleted successfully")
                           : ApiResponse.NotFound("Failure", "Address not found");
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while deleting the address");
        }
    }

    public async Task<IResult> Search(string? AddressLine1,
                                              string? AddressLine2,
                                              string? City,
                                              string? State,
                                              string? Country,
                                              string? ZipCode,
                                              HttpContext httpContext)
    {
        try
        {
            var filter = new AddressSearchFilterModel
            {
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                City = City,
                State = State,
                Country = Country,
                ZipCode = ZipCode
            };

            var addresses = await _addressService.Search(filter);
            return ApiResponse.Success("Success", "Addresses retrieved successfully with filters", addresses);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while searching for addresses");
        }
    }
}




/*

using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using order_management.database.dto;
using order_management.services.implementetions;
using order_management.services.interfaces;

namespace order_management.api;


public class AddressController
{
   
    public AddressController()
    {

    }

    public  void MapAddressRoutes( WebApplication app)
    {

        app.MapGet("/api/addresses", GetAllAddresses).RequireAuthorization();
        app.MapGet("/api/addresses/{id:guid}", GetAddressById).RequireAuthorization();
        app.MapPost("/api/addresses", AddAddress).RequireAuthorization();
        app.MapPut("/api/addresses/{id:guid}", UpdateAddress).RequireAuthorization();
        app.MapDelete("/api/addresses/{id:guid}", DeleteAddress).RequireAuthorization();
        app.MapGet("/api/addresses/search", SearchAddresses).RequireAuthorization();
    }
    public async Task<IResult> GetAllAddresses(HttpContext httpContext, IAddressService _addressService)
    {
        try
        {
            var addresses = await _addressService.GetAll();
            return ApiResponse.Success("Success", "Addresses retrieved successfully", addresses);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving addresses");
        }
    }
   
    public async Task<IResult> GetAddressById(Guid id, HttpContext httpContext, IAddressService _addressService)
    {
        try
        {
            var address = await _addressService.GetAddressById(id);
            return address == null ? ApiResponse.NotFound("Failure", "Address not found")
                                   : ApiResponse.Success("Success", "Address retrieved successfully", address);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving the address");
        }
    }


   public  async Task<IResult> AddAddress(AddressCreateModel addr, HttpContext httpContext, IAddressService _addressService, IValidator<AddressCreateModel> _createValidator)
    {
        try
        {
            if (addr == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid address data");
            }

            var validationResult = _createValidator.Validate(addr);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var createdAddress = await _addressService.CreateAddress(addr);
            return ApiResponse.Success("Success", "Address created successfully", createdAddress);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while creating the address");
        }
    }

    
   public  async Task<IResult> UpdateAddress(Guid id, AddressUpdateModel addr, HttpContext httpContext, IAddressService _addressService, IValidator<AddressUpdateModel> _updateValidator)
    {
        try
        {
            if (addr == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid address data");
            }

            var validationResult = _updateValidator.Validate(addr);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var updatedAddress = await _addressService.UpdateAddress(id, addr);
            return updatedAddress == null ? ApiResponse.NotFound("Failure", "Address not found")
                                          : ApiResponse.Success("Success", "Address updated successfully", updatedAddress);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while updating the address");
        }
    }
    
    public async Task<IResult> DeleteAddress(Guid id, HttpContext httpContext, IAddressService _addressService)
    {
        try
        {
            var success = await _addressService.DeleteAddress(id);
            return success ? ApiResponse.Success("Success", "Address deleted successfully")
                           : ApiResponse.NotFound("Failure", "Address not found");
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while deleting the address");
        }
    }
      
    public async Task<IResult> SearchAddresses(string? AddressLine1,
                                               string? AddressLine2,
                                               string? City,
                                               string? State,
                                               string? Country,
                                               string? ZipCode,
                                               HttpContext httpContext, IAddressService _addressService)
    {
        try
        {
            var filter = new AddressSearchFilterModel
            {
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                City = City,
                State = State,
                Country = Country,
                ZipCode = ZipCode
            };

            var addresses = await _addressService.SearchAddresses(filter);
            return ApiResponse.Success("Success", "Addresses retrieved successfully with filters", addresses);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while searching for addresses");
        }
    }
}











/*using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using order_management.database.dto;
using order_management.services.interfaces;

namespace order_management.api;


public class Address
{
    private readonly IAddressService _addressService;
    private readonly IValidator<AddressCreateModel> _createValidator;
    private readonly IValidator<AddressUpdateModel> _updateValidator;

    public Address(IAddressService addressService,
                            IValidator<AddressCreateModel> createValidator,
                            IValidator<AddressUpdateModel> updateValidator)
    {
        _addressService = addressService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<IResult> GetAllAddresses(HttpContext httpContext)
    {
        try
        {
            var addresses = await _addressService.GetAll();
            return ApiResponse.Success("Success", "Addresses retrieved successfully", addresses);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving addresses");
        }
    }
    [HttpGet]
    public async Task<IResult> GetAddressById(Guid id, HttpContext httpContext)
    {
        try
        {
            var address = await _addressService.GetAddressById(id);
            return address == null ? ApiResponse.NotFound("Failure", "Address not found")
                                   : ApiResponse.Success("Success", "Address retrieved successfully", address);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving the address");
        }
    }

    [HttpPost ]
    public async Task<IResult> AddAddress(AddressCreateModel addr, HttpContext httpContext)
    {
        try
        {
            if (addr == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid address data");
            }

            var validationResult = _createValidator.Validate(addr);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var createdAddress = await _addressService.CreateAddress(addr);
            return ApiResponse.Success("Success", "Address created successfully", createdAddress);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while creating the address");
        }
    }

    [HttpPut ]
    public async Task<IResult> UpdateAddress(Guid id, AddressUpdateModel addr, HttpContext httpContext)
    {
        try
        {
            if (addr == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid address data");
            }

            var validationResult = _updateValidator.Validate(addr);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var updatedAddress = await _addressService.UpdateAddress(id, addr);
            return updatedAddress == null ? ApiResponse.NotFound("Failure", "Address not found")
                                          : ApiResponse.Success("Success", "Address updated successfully", updatedAddress);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while updating the address");
        }
    }
    [HttpDelete ]
    public async Task<IResult> DeleteAddress(Guid id, HttpContext httpContext)
    {
        try
        {
            var success = await _addressService.DeleteAddress(id);
            return success ? ApiResponse.Success("Success", "Address deleted successfully")
                           : ApiResponse.NotFound("Failure", "Address not found");
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while deleting the address");
        }
    }
    [HttpGet ]  
    public async Task<IResult> SearchAddresses(string? AddressLine1,
                                               string? AddressLine2,
                                               string? City,
                                               string? State,
                                               string? Country,
                                               string? ZipCode,
                                               HttpContext httpContext)
    {
        try
        {
            var filter = new AddressSearchFilterModel
            {
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                City = City,
                State = State,
                Country = Country,
                ZipCode = ZipCode
            };

            var addresses = await _addressService.SearchAddresses(filter);
            return ApiResponse.Success("Success", "Addresses retrieved successfully with filters", addresses);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while searching for addresses");
        }
    }
}
*/

















/*// executed code 

using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using order_management.common;
using order_management.database.dto;
using order_management.services.interfaces;

namespace order_management.api
{
public static class AddressEndpoints
{
    
   
    public static async Task<IResult> GetAllAddresses(HttpContext httpContext, IAddressService _addressService)
    {
        try
        {
            var addresses = await _addressService.GetAll();
            return ApiResponse.Success("Success", "Addresses retrieved successfully", addresses);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving addresses");
        }
    }

    public static async Task<IResult> GetAddressById(Guid id, HttpContext httpContext, IAddressService _addressService)
    {
        try
        {
            var address = await _addressService.GetAddressById(id);
            return address == null ? ApiResponse.NotFound("Failure", "Address not found")
                                   : ApiResponse.Success("Success", "Address retrieved successfully", address);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving the address");
        }
    }

    public static async Task<IResult> AddAddress(AddressCreateModel addr, HttpContext httpContext, IAddressService _addressService, IValidator<AddressCreateModel> _createValidator)
    {
        try
        {
            if (addr == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid address data");
            }

            var validationResult = _createValidator.Validate(addr);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var createdAddress = await _addressService.CreateAddress(addr);
            return ApiResponse.Success("Success", "Address created successfully", createdAddress);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while creating the address");
        }
    }

    public static async Task<IResult> UpdateAddress(Guid id, AddressUpdateModel addr, HttpContext httpContext, IAddressService _addressService, IValidator<AddressUpdateModel> _updateValidator)
    {
        try
        {
            if (addr == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid address data");
            }

            var validationResult = _updateValidator.Validate(addr);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var updatedAddress = await _addressService.UpdateAddress(id, addr);
            return updatedAddress == null ? ApiResponse.NotFound("Failure", "Address not found")
                                          : ApiResponse.Success("Success", "Address updated successfully", updatedAddress);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while updating the address");
        }
    }

    public static async Task<IResult> DeleteAddress(Guid id, HttpContext httpContext, IAddressService _addressService)
    {
        try
        {
            var success = await _addressService.DeleteAddress(id);
            return success ? ApiResponse.Success("Success", "Address deleted successfully")
                           : ApiResponse.NotFound("Failure", "Address not found");
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while deleting the address");
        }
    }

    public static async Task<IResult> SearchAddresses(string? AddressLine1,
                                               string? AddressLine2,
                                               string? City,
                                               string? State,
                                               string? Country,
                                               string? ZipCode,
                                               HttpContext httpContext, IAddressService _addressService)
    {
        try
        {
            var filter = new AddressSearchFilterModel
            {
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                City = City,
                State = State,
                Country = Country,
                ZipCode = ZipCode
            };

            var addresses = await _addressService.SearchAddresses(filter);
            return ApiResponse.Success("Success", "Addresses retrieved successfully with filters", addresses);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while searching for addresses");
        }
    }
}
}
















/*using AutoMapper;
using Azure.Core;
using FluentValidation;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using order_management.common;

using order_management.database.dto;
using order_management.services.implementetions;
using order_management.services.interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace order_management.api;

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