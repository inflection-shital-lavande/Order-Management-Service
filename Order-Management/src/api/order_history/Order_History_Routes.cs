using Microsoft.AspNetCore.Builder;

namespace Order_Management.src.api.order_history
{
    public class Order_History_Routes
    {
        public void MapOrderHistoryRoutes(WebApplication app)
        {
            var order_History_Controller = new Order_History_Controller();
            var router = app.MapGroup("/api/order_History");

            router.MapGet("/", order_History_Controller.GetAll).RequireAuthorization();
            router.MapGet("/{id:guid}", order_History_Controller.GetById).RequireAuthorization();
            router.MapPost("/", order_History_Controller.Create).RequireAuthorization();
            router.MapPut("/{id:guid}", order_History_Controller.Update).RequireAuthorization();
            router.MapDelete("/{id:guid}", order_History_Controller.Delete).RequireAuthorization();
            router.MapGet("/search", order_History_Controller.Search).RequireAuthorization();
        }
    }
}