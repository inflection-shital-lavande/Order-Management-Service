using Microsoft.AspNetCore.Mvc;
using order_management.api;
using order_management.database.dto;

namespace order_management.src.api;

public  class CustomerRoutes
{
    public  void MapCustomerRoutes( WebApplication app)
    {
        // var customerController = new CustomerController();
        var router = app.MapGroup("/api/customers").WithTags("CustomerController");

        router.MapGet("/", (HttpContext httpContext, [FromServices] CustomerController customerController) => customerController.GetAll(httpContext)).RequireAuthorization();
        router.MapGet("/{id:guid}", ([FromServices] CustomerController customerController , Guid id) => customerController.GetById(id)).RequireAuthorization();
        router.MapPost("/", ([FromServices] CustomerController customerController,CustomerCreateModel Create) => customerController.Create(Create)).RequireAuthorization();
        router.MapPut("/{id:guid}", ([FromServices] CustomerController customerController,Guid id, CustomerUpdateModel customerUpdate) => customerController.Update(id,customerUpdate)).RequireAuthorization();
        router.MapDelete("/{id:guid}", ([FromServices] CustomerController customerController, Guid id) => customerController.Delete(id)).RequireAuthorization();
        router.MapGet("/Search", ([FromServices] CustomerController customerController) => customerController.Search).RequireAuthorization();
    }
}