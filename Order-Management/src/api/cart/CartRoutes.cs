using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using order_management.api;
using Order_Management.src.database.dto.cart;

namespace Order_Management.src.api.cart
{
    public class CartRoutes
    {
        public void MapCartRoutes(WebApplication app)
        {
            //  var CartController = new CartController();
            var router = app.MapGroup("/api/cart").WithTags("CartController");

             router.MapGet("/", (HttpContext context,[FromServices] CartController cartController) => cartController.GetAll(context))
               .RequireAuthorization();

            router.MapGet("/{id:guid}", ( [FromServices] CartController cartController, Guid id) => cartController.GetById( id))
                .RequireAuthorization();

            router.MapPost("/", ( [FromServices] CartController cartController, CartCreateModel cart) => cartController.Create(cart))
                .RequireAuthorization();

            router.MapPut("/{id:guid}", ( [FromServices] CartController cartController, Guid id, CartUpdateModel cart) => cartController.Update( id, cart))
                .RequireAuthorization();

            router.MapDelete("/{id:guid}", ( [FromServices] CartController cartController, Guid id) => cartController.Delete( id))
                .RequireAuthorization();

             /* router.MapGet("/search", (HttpContext context, [FromServices] CartController cartController) => cartController.Search(context))
                  .RequireAuthorization();*/


             router.MapGet("/search", (HttpContext context,[FromServices] CartController cartController,
                                                                         [FromQuery] Guid? customerId,
                                                                         [FromQuery] Guid? productId,
                                                                         [FromQuery] int? totalItemsCountGreaterThan,
                                                                         [FromQuery] int? totalItemsCountLessThan,
                                                                         [FromQuery] float? totalAmountGreaterThan,
                                                                         [FromQuery] float? totalAmountLessThan,
                                                                         [FromQuery] DateTime? createdBefore,
                                                                         [FromQuery] DateTime? createdAfter)
                     => cartController.Search(context,customerId, productId, totalItemsCountGreaterThan, totalItemsCountLessThan, totalAmountGreaterThan, totalAmountLessThan, createdBefore, createdAfter))
                  .RequireAuthorization();

            /* router.MapGet("/", CartController.GetAll).RequireAuthorization();
             router.MapGet("/{id:guid}", CartController.GetById).RequireAuthorization();
             router.MapPost("/", CartController.Create).RequireAuthorization();
             router.MapPut("/{id:guid}", CartController.Update).RequireAuthorization();
             router.MapDelete("/{id:guid}", CartController.Delete).RequireAuthorization();
             router.MapGet("/search", CartController.Search).RequireAuthorization();*/
        }
    }
}
