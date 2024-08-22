using Microsoft.AspNetCore.Builder;
using order_management.api;

namespace Order_Management.src.api.orderType
{
    public class Order_Type_Routes
    {
        public void MapOrderTypeRoutes(WebApplication app)
        {
            var orderTypeController = new Order_Type_Controller();
            var router = app.MapGroup("/api/orderType");

            router.MapGet("/", orderTypeController.GetAll).RequireAuthorization();
            router.MapGet("/{id:guid}", orderTypeController.GetById).RequireAuthorization();
            router.MapPost("/", orderTypeController.Create).RequireAuthorization();
            router.MapPut("/{id:guid}", orderTypeController.Update).RequireAuthorization();
            router.MapDelete("/{id:guid}", orderTypeController.Delete).RequireAuthorization();
            router.MapGet("/search", orderTypeController.Search).RequireAuthorization();
        }
    }
}
