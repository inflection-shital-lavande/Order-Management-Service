﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using order_management.common;
using order_management.database.dto;
using order_management.database.models;
using order_management.services.implementetions;
using order_management.services.interfaces;
using Order_Management.src.common;
using Order_Management.src.database.dto.merchant;
using Order_Management.src.services.interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace order_management.api;

public  class CustomerController: Controller
{ 

   /* private readonly ICustomerService _customerService;
    private readonly IValidator<CustomerCreateModel> _createValidator;
    private readonly IValidator<CustomerUpdateModel> _updateValidator;
    public CustomerController(ICustomerService customerService,
                            IValidator<CustomerCreateModel> createValidator,
                            IValidator<CustomerUpdateModel> updateValidator)
    {
        _customerService = customerService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }*/
    [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]

    public async Task<IResult> GetAll(HttpContext httpContext, ICustomerService _customerService)
    {
        try
        {

            var customers = await _customerService.GetAll();
           return ApiResponse.Success("Success", "Customers retrieved successfully", customers);
    }
       catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving customers");
        }
    }

    public async Task<IResult> GetById(Guid id ,ICustomerService _customerService, HttpContext httpContext)
    {
        try
        {
            var customer = await _customerService.GetById(id);
            return customer == null ? ApiResponse.NotFound("Failure", "Customer not found")
                                  : ApiResponse.Success("Success", "Customer retrieved successfully", customer);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving customers");
        }
    }
  

    public async Task<IResult> Create(CustomerCreateModel Create, ICustomerService _customerService, IValidator<CustomerCreateModel> _createValidator)
      {
          try
          {
             if (Create == null)
              {
                  return ApiResponse.BadRequest("Failure", "Invalid customer data");
              }

              var validationResult = _createValidator.Validate(Create);
              if (!validationResult.IsValid)
              {
                  return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
              }//
             /* var validationContext = new ValidationContext(Create);
              var vResult = new List<ValidationResult>();

              var isvalid = Validator.TryValidateObject(Create, validationContext, vResult, true);

              if (isvalid)
              {*/
                  var customer = await _customerService.Create(Create);
                
                  return ApiResponse.Success("Success", "Customer created successfully", customer);
             // }
             // return Results.BadRequest(vResult);
          }
          catch (ConflictException ex)
          {
              return ApiResponse.Conflict("Conflict", ex.Message);
          }
          catch (Exception ex)
          {
              return ApiResponse.Exception(ex, "Failure", "An error occurred while creating the customers");
          }
      }
   


    public async Task<IResult> Delete( Guid id,ICustomerService _customerService, HttpContext httpContext)
    {
        try
        {
            var deleteResult = await _customerService.Delete(id);
            return deleteResult == null ? ApiResponse.NotFound("Failure", "Customer not found")
                                        : ApiResponse.Success("Success", "Customer deleted successfully", deleteResult);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving customers");
        }
    }

    public async Task<IResult> Search(HttpContext httpContext, ICustomerService _customerService,
                                                   [FromQuery] string? name,
                                                   [FromQuery] string? email,
                                                   [FromQuery] string? phoneCode,
                                                   [FromQuery] string? phone,
                                                   [FromQuery] string? taxNumber,
                                                   [FromQuery] DateTime? createdBefore,
                                                   [FromQuery] DateTime? createdAfter,
                                                  [FromQuery] int? pastMonths)
    {
        try
        {

            var filter = new CustomerSearchFilterModel
            {
                Name = name,
                Email = email,
                PhoneCode = phoneCode,
                Phone = phone,
                TaxNumber = taxNumber,
                CreatedBefore = createdBefore,
                CreatedAfter = createdAfter,
                PastMonths = pastMonths
            };

            var customers = await _customerService.Search(filter);
            return customers.Items.Any()
                ? ApiResponse.Success("Success", "Customers retrieved successfully with filters", customers)
                : ApiResponse.NotFound("Failure", "No customers found matching the filters");
        }
        catch (Exception ex)
        {

            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving customers");
        }
     
    }

    public async Task<IResult> Update(ICustomerService _customerService, Guid id, IValidator<CustomerUpdateModel> _updateValidator, CustomerUpdateModel customerUpdate)
    {
        try
        {
            if (customerUpdate == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid customer data");
            }

            var validationResult = _updateValidator.Validate(customerUpdate);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var validationContext = new ValidationContext(customerUpdate);
            var vResult = new List<ValidationResult>();

            var isvalid = Validator.TryValidateObject(customerUpdate, validationContext, vResult, true);

            if (isvalid)
            {
                var customer = await _customerService.Update(id, customerUpdate);
                return customer == null ? ApiResponse.NotFound("Failure", "Customer not found")
                                             : ApiResponse.Success("Success", "Customer updated successfully");
            }
            return Results.BadRequest(vResult);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving customers");
        }
    }



}



/* public async Task<IResult> Update(Guid id, CustomerUpdateModel customerUpdate, HttpContext httpContext, ICustomerService _customerService, IValidator<CustomerUpdateModel> _updateValidator)
 {
     try
     {
         if (customerUpdate == null)
         {
             return ApiResponse.BadRequest("Failure", "Invalid customer data");
         }

         var validationResult = _updateValidator.Validate(customerUpdate);
         if (!validationResult.IsValid)
         {
             return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
         }

         var updateCustomer = await _customerService.Update(id, customerUpdate);
         return updateCustomer == null ? ApiResponse.NotFound("Failure", "Customer not found")
                                       : ApiResponse.Success("Success", "Customer updated successfully", updateCustomer);
     }
     catch (Exception ex)
     {
         return ApiResponse.Exception(ex, "Failure", "An error occurred while updating the customers");
     }
 }*/


/* public async Task<IResult> Create(CustomerCreateModel customerCreate, ICustomerService _customerService, IValidator<CustomerCreateModel> _createValidator)
   {
       try
       {
           // Check if the customer create model is null
           if (customerCreate == null)
           {
               return ApiResponse.BadRequest("Failure", "Invalid customer data");
           }

           // Perform FluentValidation on the customer create model
           var validationResult = _createValidator.Validate(customerCreate);
           if (!validationResult.IsValid)
           {
               return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
           }

           // Additionally, validate using DataAnnotations (if applicable)
           var validationContext = new ValidationContext(customerCreate);
           var validationResults = new List<ValidationResult>();
           bool isValid = Validator.TryValidateObject(customerCreate, validationContext, validationResults, true);

           // If validation is successful
           if (isValid)
           {
               // Proceed to create the customer using the service
               var customer = await _customerService.Create(customerCreate);

               // Return success response along with created customer data
               return ApiResponse.Success("Success", "Customer created successfully", customer);
           }

           // If DataAnnotations validation fails, return bad request with errors
           return Results.BadRequest(validationResults.Select(vr => vr.ErrorMessage));
       }
       catch (ConflictException ex)
       {
           // Handle specific business logic conflicts, e.g., duplicate customer entry
           return ApiResponse.Conflict("Conflict", ex.Message);
       }
       catch (Exception ex)
       {
           // Handle any other exceptions that may occur
           return ApiResponse.Exception(ex, "Failure", "An error occurred while creating the customer");
       }
   }*/

