using Microsoft.AspNetCore.Builder;
using order_management.api;

namespace Order_Management.src.api.merchant
{
    public class MerchantRoutes
    {
        public void MapMerchantRoutes(WebApplication app)
        {
            var merchantController = new MerchantController();
            var router = app.MapGroup("/api/merchants");

            router.MapGet("/", merchantController.GetAll).RequireAuthorization();
            router.MapGet("/{id:guid}", merchantController.GetById).RequireAuthorization();
            router.MapPost("/", merchantController.Create).RequireAuthorization();
            router.MapPut("/{id:guid}", merchantController.Update).RequireAuthorization();
            router.MapDelete("/{id:guid}", merchantController.Delete).RequireAuthorization();
            router.MapGet("/search", merchantController.Search).RequireAuthorization();
        }
    }
}
