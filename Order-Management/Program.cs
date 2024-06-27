using Microsoft.EntityFrameworkCore;
using Order_Management.app.database.models;
using Order_Management.Data;
using System.Net;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OrderManagementContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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


