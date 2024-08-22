using order_management.api;

namespace order_management.src.api;

public  class CustomerRoutes
{
    public  void MapCustomerRoutes( WebApplication app)
    {
        var customerController = new CustomerController();
        var router = app.MapGroup("/api/customers");

        router.MapGet("/", customerController.GetAll).RequireAuthorization();
        router.MapGet("/{id:guid}", customerController.GetById).RequireAuthorization();
        router.MapPost("/", customerController.Create).RequireAuthorization();
        router.MapPut("/{id:guid}", customerController.Update).RequireAuthorization();
        router.MapDelete("/{id:guid}", customerController.Delete).RequireAuthorization();
        router.MapGet("/Search", customerController.Search).RequireAuthorization();
    }
}