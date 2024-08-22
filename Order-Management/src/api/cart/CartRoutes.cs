using Microsoft.AspNetCore.Builder;
using order_management.api;

namespace Order_Management.src.api.cart
{
    public class CartRoutes
    {
        public void MapCartRoutes(WebApplication app)
        {
            var CartController = new CartController();
            var router = app.MapGroup("/api/cart");

            router.MapGet("/", CartController.GetAll).RequireAuthorization();
            router.MapGet("/{id:guid}", CartController.GetById).RequireAuthorization();
            router.MapPost("/", CartController.Create).RequireAuthorization();
            router.MapPut("/{id:guid}", CartController.Update).RequireAuthorization();
            router.MapDelete("/{id:guid}", CartController.Delete).RequireAuthorization();
            router.MapGet("/search", CartController.Search).RequireAuthorization();
        }
    }
}
