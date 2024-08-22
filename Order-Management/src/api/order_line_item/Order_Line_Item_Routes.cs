using Microsoft.AspNetCore.Builder;

namespace Order_Management.src.api.order_line_item
{
    public class Order_Line_Item_Routes
    {
        public void MapOrderLineItemRoutes(WebApplication app)
        {
            var order_Line_Item_Controller = new Order_Line_Item_Controller();
            var router = app.MapGroup("/api/order_Line_Items");

            router.MapGet("/", order_Line_Item_Controller.GetAll).RequireAuthorization();
            router.MapGet("/{id:guid}", order_Line_Item_Controller.GetById).RequireAuthorization();
            router.MapPost("/", order_Line_Item_Controller.Create).RequireAuthorization();
            router.MapPut("/{id:guid}", order_Line_Item_Controller.Update).RequireAuthorization();
            router.MapDelete("/{id:guid}", order_Line_Item_Controller.Delete).RequireAuthorization();
            router.MapGet("/search", order_Line_Item_Controller.Search).RequireAuthorization();
        }
    }
}
