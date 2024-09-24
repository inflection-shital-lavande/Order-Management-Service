
using AutoMapper;
using AutoMapper.Configuration;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using order_management.database.dto;
using order_management.database.models;
using order_management.services.implementetions;
using order_management.services.interfaces;
using System.ComponentModel.DataAnnotations;
//using ValidationContext = AutoMapper.Configuration.ValidationContext;
//using System.ComponentModel.DataAnnotations;

namespace order_management.api;

public class AddressController
{

    //#region Constructor
  /*  private readonly IAddressService _addressService;
    private readonly IValidator<AddressCreateModel> _createValidator;
   // private readonly IValidator<AddressUpdateModel> _updateValidator;
    public AddressController(IAddressService addressService,
                             IValidator<AddressCreateModel> createValidator)
                          //   IValidator<AddressUpdateModel> updateValidator)
    {
        _addressService = addressService;
        _createValidator = createValidator;
     //   _updateValidator = updateValidator;
    }*/
    //  #endregion
    //  #region Public Methods

   // [ProducesResponseType(200, Type = typeof(IEnumerable<Cart>))]
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
    public async Task<IResult> Create([FromBody] AddressCreateModel createaddr,  IAddressService _addressService, IValidator<AddressCreateModel> _createValidator)
    {
        try
        {
            if (createaddr == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid address data");
            }

            var validationResult = _createValidator.Validate(createaddr);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(createaddr);
            var vResult = new List<ValidationResult>();

            var isvalid = Validator.TryValidateObject(createaddr, validationContext, vResult, true);

            if (isvalid)
            { 

                var createdAddress = await _addressService.Create(createaddr);
             return ApiResponse.Success("Success", "Address created successfully", createdAddress);
            }
            return Results.BadRequest(vResult);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while creating the address");
        }
    }
    public async Task<IResult> Update(Guid id, AddressUpdateModel addr,  IAddressService _addressService, IValidator<AddressUpdateModel> _updateValidator)//,HttpContext httpContext)//HttpContext httpContext,
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
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(addr);
            var vResult = new List<ValidationResult>();

            var isvalid = Validator.TryValidateObject(addr, validationContext, vResult, true);

            if (isvalid)
            {
                var customer = await _addressService.Update(id, addr);
                return customer == null ? ApiResponse.NotFound("Failure", "Customer not found")
                                             : ApiResponse.Success("Success", "Customer updated successfully");
            }
            return Results.BadRequest(vResult);

            /*  var updatedAddress = await _addressService.Update(id, addr);
              return updatedAddress == null ? ApiResponse.NotFound("Failure", "Address not found")
                                            : ApiResponse.Success("Success", "Address updated successfully", updatedAddress);*/
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

    public async Task<IResult> Search(
                                              string? AddressLine1,
                                              string? AddressLine2,
                                              string? City,
                                              string? State,
                                              string? Country,
                                              string? ZipCode
                                              , IAddressService _addressService)
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
                ? ApiResponse.Success("Success", "Addresses retrieved successfully with filters", addresses)
                : ApiResponse.NotFound("Failure", "No addresses found matching the filters");
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while searching for addresses");
        }
    }


}
