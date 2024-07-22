using Microsoft.EntityFrameworkCore;
using Order_Management.app.Config;
using Order_Management.app.database.models;
using Order_Management.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Order_Management.app.api;
using Order_Management.app.database.service;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OrderManagementService.app.api.Authen;
using Order_Management.Auth;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OrderManagementContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingProfile));
//builder.Services.AddValidatorsFromAssemblyContaining<Program>();
//add services
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

//authentication

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddSwaggerGen(Swagger =>
{
    Swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "asp.net 8 web api ",
        Description = "authentication"
    });
    Swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });
    Swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference =new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },Array.Empty<string>()
        }

});
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.Use(async (context, next) =>
{
    await next.Invoke();
    if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
    {
        context.Response.ContentType = "application/json";
        var response = new { Message = "Unauthorized access. Please provide valid credentials." };
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
});

app.UseAuthorization();


// Register endpoints
app.MapAuthEndpoints();
app.MapAddressEndpoints();


app.MapCustomerEndpoints();

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
/

*/