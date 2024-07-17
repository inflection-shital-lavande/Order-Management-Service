using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order_Management.app.database.service;
using Order_Management.app.domain_types.dto.cutomerModelDTO;
using Order_Management.app.domain_types.dto;
using Microsoft.AspNetCore.Authorization;

namespace Order_Management.app.api
{
    public static class CustomerEndpoints
    {
        
        public static void MapCustomerEndpoints(this WebApplication app)
        {
            app.MapGet("/OrderManagementService/Customer", async (ICustomerService customerService) =>
            {
                
                   
                    var customers = await customerService.GetAllCustomersAsync();
                    return Results.Ok(new
                    {
                        Message = "Customers retrieved successfully",
                        Data = customers
                    });
                
            });

            app.MapGet("/OrderManagementService/Customer/{id:guid}", async (ICustomerService customerService, Guid id) =>
            {
                var customer = await customerService.GetCustomerByIdAsync(id);
                if (customer == null)
                {
                    return Results.NotFound(new { Message = "Customer not found" });
                }

                return Results.Ok(new
                {
                    Message = "Customer retrieved successfully",
                    Data = customer
                });
            });

            app.MapPost("/OrderManagementService/AddCustomer", async (ICustomerService customerService, customerCreateDTO customerDto) =>
            {
                var createdCustomer = await customerService.CreateCustomerAsync(customerDto);

                return Results.Created($"/OrderManagementService/Customer/{createdCustomer.Id}", new
                {
                    Message = "Customer created successfully",
                    Data = createdCustomer
                });
            });

            app.MapPut("/OrderManagementService/UpdateCustomer/{id:guid}", async (ICustomerService customerService, Guid id, customerUpdateDTO customerDto) =>
            {
                var updatedCustomer = await customerService.UpdateCustomerAsync(id, customerDto);
                if (updatedCustomer == null)
                {
                    return Results.NotFound(new { Message = "Customer not found" });
                }

                return Results.Ok(new { Message = "Customer updated successfully" });
            });

            app.MapDelete("/OrderManagementService/Customer/{id:guid}", async (ICustomerService customerService, Guid id) =>
            {
                var deleteResult = await customerService.DeleteCustomerAsync(id);
                if (!deleteResult)
                {
                    return Results.NotFound(new { Message = "Customer not found" });
                }

                return Results.Ok(new { Message = "Customer deleted successfully" });
            });

            app.MapGet("/OrderManagementService/SearchCustomer", async (ICustomerService customerService,
                                                                       [FromQuery] string? Name,
                                                                       [FromQuery] string? Email,
                                                                       [FromQuery] string? PhoneCode,
                                                                       [FromQuery] string? Phone,
                                                                       [FromQuery] string? TaxNumber,
                                                                       [FromQuery] DateTime? CreatedBefore,
                                                                       [FromQuery] DateTime? CreatedAfter,
                                                                       [FromQuery] int? PastMonths) =>
            {
                var filterDTO = new customerSearchFilterDTO
                {
                    Name = Name,
                    Email = Email,
                    PhoneCode = PhoneCode,
                    Phone = Phone,
                    TaxNumber = TaxNumber,
                    CreatedBefore = CreatedBefore,
                    CreatedAfter = CreatedAfter,
                    PastMonths = PastMonths
                };

                var customers = await customerService.SearchCustomersAsync(filterDTO);

                return Results.Ok(new
                {
                    Message = "Customers retrieved successfully with filters",
                    Data = customers
                });
            });
        }
    }
}
