using Microsoft.AspNetCore.Builder;

namespace Order_Management.src.api.order
{
    public class OrderRoutes
    {
        public void MapOrderRoutes(WebApplication app)
        {
            var orderController = new OrderController();
            var router = app.MapGroup("/api/orders");

            router.MapGet("/", orderController.GetAll).RequireAuthorization();
            router.MapGet("/{id:guid}", orderController.GetById).RequireAuthorization();
            router.MapPost("/", orderController.Create).RequireAuthorization();
            router.MapPut("/{id:guid}", orderController.Update).RequireAuthorization();
            router.MapDelete("/{id:guid}", orderController.Delete).RequireAuthorization();
            router.MapGet("/search", orderController.Search).RequireAuthorization();
            router.MapPut("{id:guid}/status", orderController.UpdateOrderStatus);
        }
    }
}
