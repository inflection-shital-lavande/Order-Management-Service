using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order_Management.app.database.service;
using Order_Management.app.domain_types.dto;
using Microsoft.AspNetCore.Authorization;
using Order_Management.app.database.models;

namespace Order_Management.app.api
{
    public static class AddressEndpoints
    {
        
        public static void MapAddressEndpoints(this WebApplication app)
        {
            app.MapGet("/OrderManagementService/GetAllAddress", async (IAddressService addressService) =>
            {
               
                    var addresses = await addressService.GetAll();
                    return Results.Ok(new
                    {
                        Message = "Addresses retrieved successfully",
                        Data = addresses
                    });
                
            }).RequireAuthorization();

            app.MapGet("/OrderManagementService/GetAddress/{id:guid}", async (IAddressService addressService, Guid id) =>
            {
                var address = await addressService.GetAddressByIdAsync(id);
                if (address == null)
                {
                    return Results.NotFound(new { Message = "Address not found" });
                }

                return Results.Ok(new
                {
                    Message = "Address retrieved successfully",
                    Data = address
                });
            }).RequireAuthorization();

            app.MapPost("/OrderManagementService/AddAddress", async (IAddressService addressService, addressCreateDTO addrDTO) =>
            {
                var createdAddress = await addressService.CreateAddressAsync(addrDTO);

                return Results.Created($"/OrderManagementService/Address/{createdAddress}", new
                {
                    Message = "Address created successfully",
                    Data = createdAddress
                });
            }).RequireAuthorization();

            app.MapPut("/OrderManagementService/UpdateAddress/{id:guid}", async (IAddressService addressService, Guid id, addressUpdateDTO addrDTO) =>
            {
                var updatedAddress = await addressService.UpdateAddressAsync(id, addrDTO);
                if (updatedAddress == null)
                {
                    return Results.NotFound(new { Message = "Address not found" });
                }

                return Results.Ok(new { Message = "Address updated successfully" });
            }).RequireAuthorization();

            app.MapDelete("/OrderManagementService/DeleteAddress/{id:guid}", async (IAddressService addressService, Guid id) =>
            {

                var addresses = await addressService.DeleteAddressAsync(id);
                return Results.Ok(new
                {
                    Message = "Addresses deleted successfully",
                    Data = addresses
                });
            }).RequireAuthorization();

            app.MapGet("/OrderManagementService/SearchAddress", async (IAddressService addressService,
                                                                       [FromQuery] string? AddressLine1,
                                                                       [FromQuery] string? City,
                                                                       [FromQuery] string? State,
                                                                       [FromQuery] string? Country,
                                                                       [FromQuery] string? ZipCode) =>
            {
                var filterDTO = new addressSearchFilterDTO
                {
                    AddressLine1 = AddressLine1,
                    City = City,
                    State = State,
                    Country = Country,
                    ZipCode = ZipCode
                };

                var addresses = await addressService.SearchAddressesAsync(filterDTO);

                return Results.Ok(new
                {
                    Message = "Addresses retrieved successfully with filters",
                    Data = addresses
                });
            }).RequireAuthorization();
        }
    }
}

/*

using Microsoft.EntityFrameworkCore;
using Order_Management.app.database.models;
using Order_Management.Data;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order_Management.app.domain_types.dto;

namespace Order_Management.app.api
{
    public static class AddressEndpoints
    {
        public static void MapAddressEndpoints(this WebApplication app, IMapper mapper)
        {
            // GET all addresses
            app.MapGet("/OrderManagementService/Address", async (OrderManagementContext db) =>
            {
                var addresses = await db.Addresses.ToListAsync();
                var responseDTOs = mapper.Map<List<addressResponseDTO>>(addresses);
                return Results.Ok(new
                {
                    Message = "Addresses retrieved successfully",
                    Data = responseDTOs
                });
            });

            // GET address by ID
            app.MapGet("/OrderManagementService/Address/{id:guid}", async (OrderManagementContext db, Guid id) =>
            {
                var address = await db.Addresses.FindAsync(id);
                if (address == null)
                {
                    return Results.NotFound(new { Message = "Address not found" });
                }

                var responseDTO = mapper.Map<addressResponseDTO>(address);
                return Results.Ok(new
                {
                    Message = "Address retrieved successfully",
                    Data = responseDTO
                });
            });

            // POST create new address
            app.MapPost("/OrderManagementService/AddAddress", async (OrderManagementContext db, addressCreateDTO addrDTO) =>
            {
                var addr = mapper.Map<Address>(addrDTO);
                addr.Id = Guid.NewGuid();
                addr.CreatedAt = DateTime.UtcNow;
                addr.UpdatedAt = DateTime.UtcNow;

                db.Addresses.Add(addr);
                await db.SaveChangesAsync();

                var responseDTO = mapper.Map<addressResponseDTO>(addr);
                return Results.Created($"/OrderManagementService/Address/{addr.Id}", new
                {
                    Message = "Address created successfully",
                    Data = responseDTO
                });
            });

            // PUT update address by ID
            app.MapPut("/OrderManagementService/UpdateAddress/{id:guid}", async (OrderManagementContext db, Guid id, addressUpdateDTO addrDTO) =>
            {
                var address = await db.Addresses.FindAsync(id);
                if (address == null)
                {
                    return Results.NotFound(new { Message = "Address not found" });
                }

                mapper.Map(addrDTO, address);
                address.UpdatedAt = DateTime.UtcNow;

                db.Addresses.Update(address);
                await db.SaveChangesAsync();

                return Results.Ok(new { Message = "Address updated successfully" });
            });

            // DELETE address by ID
            app.MapDelete("/OrderManagementService/Address/{id:guid}", async (OrderManagementContext db, Guid id) =>
            {
                var address = await db.Addresses.FindAsync(id);
                if (address == null)
                {
                    return Results.NotFound(new { Message = "Address not found" });
                }

                db.Addresses.Remove(address);
                await db.SaveChangesAsync();
                return Results.Ok(new { Message = "Address deleted successfully" });
            });

            app.MapGet("/OrderManagementService/SearchAddress", async (OrderManagementContext db,
                                                                       [FromQuery] string? AddressLine1,
                                                                       [FromQuery] string? City,
                                                                       [FromQuery] string? State,
                                                                       [FromQuery] string? Country,
                                                                       [FromQuery] string? ZipCode) =>
            {
                var filterDTO = new addressSearchFilterDTO
                {
                    AddressLine1 = AddressLine1,
                    City = City,
                    State = State,
                    Country = Country,
                    ZipCode = ZipCode
                };

                var query = db.Addresses.AsQueryable();

                if (!string.IsNullOrEmpty(filterDTO.AddressLine1))
                {
                    query = query.Where(a => a.AddressLine1.Contains(filterDTO.AddressLine1));
                }

                if (!string.IsNullOrEmpty(filterDTO.City))
                {
                    query = query.Where(a => a.City.Contains(filterDTO.City));
                }

                if (!string.IsNullOrEmpty(filterDTO.State))
                {
                    query = query.Where(a => a.State.Contains(filterDTO.State));
                }

                if (!string.IsNullOrEmpty(filterDTO.Country))
                {
                    query = query.Where(a => a.Country.Contains(filterDTO.Country));
                }

                if (!string.IsNullOrEmpty(filterDTO.ZipCode))
                {
                    query = query.Where(a => a.ZipCode.Contains(filterDTO.ZipCode));
                }

                var addresses = await query.ToListAsync();
                var responseDTOs = mapper.Map<List<addressResponseDTO>>(addresses);

                return Results.Ok(new
                {
                    Message = "Addresses retrieved successfully with filters",
                    Data = responseDTOs
                });
            });


        }
    }
}
*/