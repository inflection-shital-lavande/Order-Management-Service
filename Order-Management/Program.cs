using Microsoft.EntityFrameworkCore;

using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using order_management.auth;
using System.Text.Json;
using order_management.database;
using order_management.config;
using order_management.services.interfaces;
using order_management.services.implementetions;
using order_management.src.api;
using FluentValidation;
using order_management.database.dto;
using static order_management.src.api.address.AddressValidation;
using static order_management.src.api.auth.AuthenticationValidation;
using static order_management.src.api.coupons.CouponsValidation;
using static order_management.src.api.customer.CustomerValidation;
using order_management.common;
using Microsoft.AspNetCore.Http.HttpResults;
using order_management.api;
using Order_Management.src.services.implementetions;
using Order_Management.src.services.interfaces;
using Order_Management.src.api.File;
using order_management.src.services.interfaces;
using order_management.src.services.implementetions;
using Order_Management.src.api.cart;
using Order_Management.src.api.merchant;
using Order_Management.src.api.order;
using Order_Management.src.api.order_history;
using Order_Management.src.api.order_line_item;
using Order_Management.src.api.orderType;
using Order_Management.src.api.payment_transaction;
using Order_Management.src.database.dto.cart;
using static Order_Management.src.api.cart.CartValidation;
using static Order_Management.src.api.merchant.MerchantValidation;
using Order_Management.src.database.dto.merchant;
using static Order_Management.src.api.order.OrderValidation;
using order_management.src.database.dto;
using static Order_Management.src.api.order_history.Order_History_Validation;
using order_management.src.database.dto.orderHistory;
using static Order_Management.src.api.order_line_item.Order_Line_Item_Validation;
using Order_Management.src.database.dto.order_line_item;
using static Order_Management.src.api.orderType.Order_Type_Validation;
using Order_Management.src.database.dto.orderType;
using static Order_Management.src.api.payment_transaction.Payment_Transection_Validation;
using Order_Management.src.database.dto.payment_transaction;
using Order_Management.src.api.customerAddress;
using static Order_Management.src.api.customerAddress.CustomerAddressValidation;


var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OrderManagementContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAntiforgery();  // Ensure this is added if anti-forgery is required

//add services
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IMerchantService, MerchantService>();
builder.Services.AddScoped<IOrderHistoryService, OrderHistoryService>();
builder.Services.AddScoped<IOrderLineItem, OrderLineItemService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderTypeService, OrderTypeService>();
builder.Services.AddScoped<IPaymentTransactionService, PaymentTransactionService>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();    

builder.Services.AddTransient<AddressRoutes>();
//builder.Services.AddTransient<AddressController>();

builder.Services.AddTransient<AuthenticationRoutes>();
builder.Services.AddTransient<CouponsRoutes>();
builder.Services.AddTransient<CustomerRoutes>();
builder.Services.AddTransient<CartRoutes>();
builder.Services.AddTransient<MerchantRoutes>();
builder.Services.AddTransient<OrderRoutes>();
builder.Services.AddTransient<Order_History_Routes>();
builder.Services.AddTransient<Order_Line_Item_Routes>();
builder.Services.AddTransient<Order_Type_Routes>();
builder.Services.AddTransient<Payment_Transaction_Routes>();
builder.Services.AddTransient<FileUploadRoutes>();
builder.Services.AddTransient<CustomerAddressRoutes>();


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
       // Description = "authentication"
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


//FluentValidation
//Address
builder.Services.AddScoped<IValidator<AddressCreateModel>, AddAddressDTOValidator>();
builder.Services.AddScoped<IValidator<AddressUpdateModel>, UpdateAddressDTOValidator>();

builder.Services.AddScoped<IValidator<CouponCreateModel>, AddCouponsDTOValidator>();
builder.Services.AddScoped<IValidator<CouponUpdateModel>, UpdateCouponDTOValidator>();

builder.Services.AddScoped<IValidator<CustomerCreateModel>, AddCustomerDTOValidator>();
builder.Services.AddScoped<IValidator<CustomerUpdateModel>, UpdateCustomerDTOValidator>();

builder.Services.AddScoped<IValidator<CartCreateModel>, AddCartDTOValidator>();
builder.Services.AddScoped<IValidator<CartUpdateModel>, UpdateCartDTOValidator>();

builder.Services.AddScoped<IValidator<MerchantCreateModel>, AddMerchantDTOValidator>();
builder.Services.AddScoped<IValidator<MerchantUpdateModel>, UpdateMerchantDTOValidator>();

builder.Services.AddScoped<IValidator<OrderCreateModel>, AddOrderDTOValidator>();
builder.Services.AddScoped<IValidator<OrderUpdateModel>, UpdateOrderDTOValidator>();

builder.Services.AddScoped<IValidator<OrderHistoryCreateModel>, AddOrderHistoryDTOValidator>();
builder.Services.AddScoped<IValidator<OrderHistoryUpdateModel>, UpdateOrderHistoryDTOValidator>();

builder.Services.AddScoped<IValidator<OrderLineItemCreateModel>, AddOrderLineItemDTOValidator>();
builder.Services.AddScoped<IValidator<OrderLineItemUpdateModel>, UpdateOrderLineItemDTOValidator>();

builder.Services.AddScoped<IValidator<OrderTypeCreateModel>, AddOrdeTypeDTOValidator>();
builder.Services.AddScoped<IValidator<OrderTypeUpdateModel>, UpdateOrderTypeDTOValidator>();

builder.Services.AddScoped<IValidator<PaymentTransactionCreateModel>, AddPaymentTransactionDTOValidator>();
builder.Services.AddScoped<IValidator<CustomerAddressCreateDTO>, AddCADTOValidator>();

//Auth
builder.Services.AddScoped<IValidator<RegisterDTO>, RegisterDTOValidator>();
builder.Services.AddScoped<IValidator<LoginDTO>, LoginDTOValidator>();






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAntiforgery(); // Apply anti-forgery middleware here


app.UseAuthentication();

app.Use(async (context, next) =>
{
    await next.Invoke();
    if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
    {
        context.Response.ContentType = "application/json";
        var response = new { Status = "Error", Message = "Required Authentication.", HttpCode = 401, };
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
});
app.UseAuthorization();

// Use the AddressEndpoints to configure routes
//correct code
//var addressEndpoints = app.Services.GetRequiredService<AddressController>();
//addressEndpoints.MapAddressRoutes(app);


var addressEndpoints = app.Services.GetRequiredService<AddressRoutes>();
addressEndpoints.MapRoutes(app);

var authEndpoints = app.Services.GetRequiredService<AuthenticationRoutes>();
authEndpoints.MapAuthRoutes(app);

var couponsEndpoints = app.Services.GetRequiredService<CouponsRoutes>();
couponsEndpoints.MapCouponsRoutes(app);

var customerEndpoints = app.Services.GetRequiredService<CustomerRoutes>();
customerEndpoints.MapCustomerRoutes(app);

var cartEndpoints = app.Services.GetRequiredService<CartRoutes>();
cartEndpoints.MapCartRoutes(app);

var merchantEndpoints = app.Services.GetRequiredService<MerchantRoutes>();
merchantEndpoints.MapMerchantRoutes(app);

var orderEndpoints = app.Services.GetRequiredService<OrderRoutes>();
orderEndpoints.MapOrderRoutes(app);

var orderHistoryEndpoints = app.Services.GetRequiredService<Order_History_Routes>();
orderHistoryEndpoints.MapOrderHistoryRoutes(app);

var orderLineItemEndpoints = app.Services.GetRequiredService<Order_Line_Item_Routes>();
orderLineItemEndpoints.MapOrderLineItemRoutes(app);

var orderTypeEndpoints = app.Services.GetRequiredService<Order_Type_Routes>();
orderTypeEndpoints.MapOrderTypeRoutes(app);

var paymentTransactionEndpoints = app.Services.GetRequiredService<Payment_Transaction_Routes>();
paymentTransactionEndpoints.MapPaymentTransectionRoutes(app);

var fileUploadEndpoints = app.Services.GetRequiredService < FileUploadRoutes>();
fileUploadEndpoints.MapFileUploadRoutes(app);

var customerAddress = app.Services.GetRequiredService<CustomerAddressRoutes>();
customerAddress.MapCARoutes(app);




app.Run();




/*
using Microsoft.EntityFrameworkCore;

using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using order_management.auth;
using System.Text.Json;
using order_management.database;
using order_management.config;
using order_management.services.interfaces;
using order_management.services.implementetions;
using order_management.src.api;
using FluentValidation;
using order_management.database.dto;
using static order_management.src.api.address.AddressValidation;
using static order_management.src.api.auth.AuthenticationValidation;
using static order_management.src.api.coupons.CouponsValidation;
using static order_management.src.api.customer.CustomerValidation;
using order_management.common;
using Microsoft.AspNetCore.Http.HttpResults;
using order_management.api;


var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OrderManagementContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingProfile));

//add services
builder.Services.AddTransient<IAddressService, AddressService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICouponService, CouponService>();


builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();


builder.Services.AddTransient<AddressRoutes>();
builder.Services.AddTransient<AddressController>();

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
       // Description = "authentication"
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


//FluentValidation
//Address
builder.Services.AddScoped<IValidator<AddressCreateModel>, AddAddressDTOValidator>();
builder.Services.AddScoped<IValidator<AddressUpdateModel>, UpdateAddressDTOValidator>();

builder.Services.AddScoped<IValidator<CouponCreateModel>, AddCouponsDTOValidator>();
builder.Services.AddScoped<IValidator<CouponUpdateModel>, UpdateCouponDTOValidator>();

builder.Services.AddScoped<IValidator<CustomerCreateModel>, AddCustomerDTOValidator>();
builder.Services.AddScoped<IValidator<CustomerUpdateModel>, UpdateCustomerDTOValidator>();


//Auth
builder.Services.AddScoped<IValidator<RegisterDTO>, RegisterDTOValidator>();
builder.Services.AddScoped<IValidator<LoginDTO>, LoginDTOValidator>();




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
        var response = new { Status = "Error", Message = "Required Authentication.", HttpCode = 401, };
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
});
app.UseAuthorization();


/*
using (var scope = app.Services.CreateScope())
{
    var addressRoutes = scope.ServiceProvider.GetRequiredService<AddressRoutes>();
    addressRoutes.MapRoutes(app);
}*

var addressRoutes = app.Services.GetRequiredService<AddressRoutes>();
addressRoutes.MapRoutes(app);




app.Run();

*/



