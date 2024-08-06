using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using Order_Management.database.dto;
using Order_Management.services.implementetions;
using Order_Management.services.interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace Order_Management.api
{
    public static class CustomerController
    {
        public static async Task<IResult> GetAllCustomers(ICustomerService customerService)
        {
            try
            {
                var customers = await customerService.GetAllCustomersAsync();
                return ApiResponse.Success("Success", "Customers retrieved successfully", customers);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure","not found");
            }
        }

        public static async Task<IResult> GetCustomerById(ICustomerService customerService, Guid id)
        {
            try
            {
                var customer = await customerService.GetCustomerByIdAsync(id);
                return customer == null ? ApiResponse.NotFound("Failure", "Customer not found") : ApiResponse.Success("Success", "Customer retrieved successfully", customer);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "not found");
            }
        }

        public static async Task<IResult> AddCustomer(ICustomerService customerService, IValidator<CustomerCreateDTO> validator, CustomerCreateDTO customerDto)
        {
            try
            {
                if (customerDto == null)
                {
                    return ApiResponse.BadRequest("Failure", "Invalid Customer data");
                }
                var validationResult = validator.Validate(customerDto);
                if (!validationResult.IsValid)
                {
                    return ApiResponse.BadRequest("Failure",validationResult.Errors.Select(e => e.ErrorMessage));
                }

                var validationContext = new ValidationContext(customerDto);
                var vResult = new List<ValidationResult>();

                var isvalid = Validator.TryValidateObject(customerDto, validationContext, vResult, true);

                if (isvalid)
                {
                    var createdCustomer = await customerService.CreateCustomerAsync(customerDto);
                    return ApiResponse.Created("Success", "Customer created successfully", createdCustomer);
                }
                return Results.BadRequest(vResult);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "not found");
            }
        }

        public static async Task<IResult> UpdateCustomer(ICustomerService customerService, Guid id, IValidator<CustomerUpdateDTO> validator, CustomerUpdateDTO customerDto)
        {
            try
            {
                if (customerDto == null)
                {
                    return ApiResponse.BadRequest("Failure", "Invalid Customer data");
                }

                var validationResult = validator.Validate(customerDto);
                if (!validationResult.IsValid)
                {
                    return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
                }

                var validationContext = new ValidationContext(customerDto);
                var vResult = new List<ValidationResult>();

                var isvalid = Validator.TryValidateObject(customerDto, validationContext, vResult, true);

                if (isvalid)
                {
                    var updatedCustomer = await customerService.UpdateCustomerAsync(id, customerDto);
                    return updatedCustomer == null ? ApiResponse.NotFound("Failure", "Customer not found") : ApiResponse.Success("Success", "Customer updated successfully");
                }
                return Results.BadRequest(vResult);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "not found");
            }
        }

        public static async Task<IResult> DeleteCustomer(ICustomerService customerService, Guid id)
        {
            try
            {
                var deleteResult = await customerService.DeleteCustomerAsync(id);
                return deleteResult == null ? ApiResponse.NotFound("Failure", "Customer not found") : ApiResponse.Success("Success", "Customer deleted successfully", deleteResult);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "not found");
            }
        }

        public static async Task<IResult> SearchCustomers(ICustomerService customerService,
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
                var filterDTO = new CustomerSearchFilterDTO
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

                var customers = await customerService.SearchCustomersAsync(filterDTO);
                return ApiResponse.Success("Success", "Customers retrieved successfully with filters", customers);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "not found");
            }
        }
    }
}

