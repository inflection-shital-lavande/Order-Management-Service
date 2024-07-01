using Microsoft.EntityFrameworkCore;
using Order_Management.app.Config;
using Order_Management.app.database.models;
using Order_Management.app.database.models.DTO;
using Order_Management.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OrderManagementContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var mapper = app.Services.GetRequiredService<IMapper>();

// Address Endpoints
app.MapGet("/OrderManagementService/Address", async (OrderManagementContext db) =>
{
    return await db.Addresses.ToListAsync();
});

app.MapGet("/OrderManagementService/Address/{id:guid}", async (OrderManagementContext db, Guid id) =>
{
    var address = await db.Addresses.FindAsync(id);
    return address != null ? Results.Ok(address) : Results.NotFound();
});

app.MapPost("/OrderManagementService/AddAddress", async (OrderManagementContext db, AddressDTO addrDTO) =>
{
    var addr = mapper.Map<Address>(addrDTO);

    addr.Id = Guid.NewGuid();
    addr.CreatedAt = DateTime.UtcNow;
    addr.UpdatedAt = DateTime.UtcNow;

    db.Addresses.Add(addr);
    await db.SaveChangesAsync();

    return Results.Created($"/OrderManagementService/Address/{addr.Id}", addr);
});

app.MapPut("/OrderManagementService/UpdateAddress/{id:guid}", async (OrderManagementContext db, Guid id, AddressDTO addrDTO) =>
{
    var address = await db.Addresses.FindAsync(id);
    if (address == null)
    {
        return Results.NotFound();
    }

    mapper.Map(addrDTO, address);
    address.UpdatedAt = DateTime.UtcNow;

    db.Addresses.Update(address);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/OrderManagementService/Address/{id:guid}", async (OrderManagementContext db, Guid id) =>
{
    var address = await db.Addresses.FindAsync(id);
    if (address == null)
    {
        return Results.NotFound();
    }

    db.Addresses.Remove(address);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Customer Endpoints
app.MapGet("/OrderManagementService/Customer", async (OrderManagementContext db) =>
{
    return await db.Customers.ToListAsync();
});

app.MapGet("/OrderManagementService/Customer/{id:guid}", async (OrderManagementContext db, Guid id) =>
{
    var customer = await db.Customers.FindAsync(id);
    return customer != null ? Results.Ok(customer) : Results.NotFound();
});

app.MapPost("/OrderManagementService/AddCustomer", async (OrderManagementContext db, CustomerDTO customerDTO) =>
{
    var customer = mapper.Map<Customer>(customerDTO);

    customer.Id = Guid.NewGuid();
    customer.CreatedAt = DateTime.UtcNow;

    db.Customers.Add(customer);
    await db.SaveChangesAsync();

    return Results.Created($"/OrderManagementService/Customer/{customer.Id}", customer);
});

app.MapPut("/OrderManagementService/UpdateCustomer/{id:guid}", async (OrderManagementContext db, Guid id, CustomerDTO customerDTO) =>
{
    var customer = await db.Customers.FindAsync(id);
    if (customer == null)
    {
        return Results.NotFound();
    }

    mapper.Map(customerDTO, customer);
    customer.UpdatedAt = DateTime.UtcNow;

    db.Customers.Update(customer);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/OrderManagementService/Customer/{id:guid}", async (OrderManagementContext db, Guid id) =>
{
    var customer = await db.Customers.FindAsync(id);
    if (customer == null)
    {
        return Results.NotFound();
    }

    db.Customers.Remove(customer);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// CustomerAddress Endpoints
app.MapGet("/OrderManagementService/CustomerAddress", async (OrderManagementContext db) =>
{
    return await db.CustomerAddresses.ToListAsync();
});

app.MapGet("/OrderManagementService/CustomerAddress/{customerId:guid}/{addressId:guid}", async (OrderManagementContext db, Guid customerId, Guid addressId) =>
{
    var customerAddress = await db.CustomerAddresses.FindAsync(customerId, addressId);
    return customerAddress != null ? Results.Ok(customerAddress) : Results.NotFound();
});

app.MapPost("/OrderManagementService/AddCustomerAddress", async (OrderManagementContext db, CustomerAddress customerAddress) =>
{
    customerAddress.Id = Guid.NewGuid();

    db.CustomerAddresses.Add(customerAddress);
    await db.SaveChangesAsync();

    return Results.Created($"/OrderManagementService/CustomerAddress/{customerAddress.CustomerId}/{customerAddress.AddressId}", customerAddress);
});

app.MapPut("/OrderManagementService/UpdateCustomerAddress/{customerId:guid}/{addressId:guid}", async (OrderManagementContext db, Guid customerId, Guid addressId, CustomerAddress updatedCustomerAddress) =>
{
    var customerAddress = await db.CustomerAddresses.FindAsync(customerId, addressId);
    if (customerAddress == null)
    {
        return Results.NotFound();
    }

    customerAddress.AddressType = updatedCustomerAddress.AddressType;
    customerAddress.IsFavorite = updatedCustomerAddress.IsFavorite;
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/OrderManagementService/CustomerAddress/{customerId:guid}/{addressId:guid}", async (OrderManagementContext db, Guid customerId, Guid addressId) =>
{
    var customerAddress = await db.CustomerAddresses.FindAsync(customerId, addressId);
    if (customerAddress == null)
    {
        return Results.NotFound();
    }

    db.CustomerAddresses.Remove(customerAddress);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();

/*using Microsoft.EntityFrameworkCore;
using Order_Management.app.Config;
using Order_Management.app.database.models;
using Order_Management.Data;
using System.Net;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OrderManagementContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Get all addresses
app.MapGet("/OrderManagementService/Address", async (OrderManagementContext db) =>
{

    return await db.Addresses.ToListAsync();

});

// Get address by ID
app.MapGet("/OrderManagementService/Address/{id:guid}", async (OrderManagementContext db, Guid id) =>
{
    var address = await db.Addresses.FindAsync(id);
    return address != null ? Results.Ok(address) : Results.NotFound();
});

// Add address record
app.MapPost("/OrderManagementService/AddAddress", async (OrderManagementContext db, Address addr) =>
{
    try
    {
        // Ensure required fields are set
        if (string.IsNullOrEmpty(addr.AddressLine1) || string.IsNullOrEmpty(addr.City) ||
            string.IsNullOrEmpty(addr.State) || string.IsNullOrEmpty(addr.Country) || string.IsNullOrEmpty(addr.ZipCode))
        {
            return Results.BadRequest("Required fields are missing.");
        }

        addr.Id = Guid.NewGuid(); // Ensure a new GUID is generated for each new address
        addr.CreatedAt = DateTime.UtcNow; // Set CreatedAt to the current UTC time
        addr.UpdatedAt = DateTime.UtcNow; // Set UpdatedAt to the current UTC time

        db.Addresses.Add(addr);
        await db.SaveChangesAsync();

        return Results.Created($"/OrderManagementService/Address/{addr.Id}", addr);
    }
    catch (DbUpdateException ex)
    {
        app.Logger.LogError(ex, "Database update error occurred while adding the address.");
        return Results.Problem("Database update error occurred while adding the address.");
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An unexpected error occurred while adding the address.");
        return Results.Problem("An unexpected error occurred while adding the address.");
    }
});

// Update address record
app.MapPut("/OrderManagementService/UpdateAddress/{id:guid}", async (OrderManagementContext db, Guid id, Address addr) =>
{
    var address = await db.Addresses.FindAsync(id);
    if (address == null)
    {
        return Results.NotFound();
    }

    address.AddressLine1 = addr.AddressLine1;
    address.AddressLine2 = addr.AddressLine2;
    address.City = addr.City;
    address.State = addr.State;
    address.Country = addr.Country;
    address.ZipCode = addr.ZipCode;
    address.CreatedBy = addr.CreatedBy;
    address.CreatedAt = addr.CreatedAt;
    address.UpdatedAt = DateTime.UtcNow;

    db.Addresses.Update(address);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

// Delete address record
app.MapDelete("/OrderManagementService/Address/{id:guid}", async (OrderManagementContext db, Guid id) =>
{
    var address = await db.Addresses.FindAsync(id);
    if (address == null)
    {
        return Results.NotFound();
    }

    db.Addresses.Remove(address);
    await db.SaveChangesAsync();
    return Results.NoContent();
});




app.Run();


*/