using Microsoft.AspNetCore.Builder;
using order_management.api;

namespace order_management.src.api;

public  class CouponsRoutes
{
    public  void MapCouponsRoutes( WebApplication app)
    {
        var couponsController = new CouponsController();
        var router = app.MapGroup("/api/coupons");

        router.MapGet("/", couponsController.GetAll).RequireAuthorization();
        router.MapGet("/{id:guid}", couponsController.GetById).RequireAuthorization();
        router.MapPost("/", couponsController.Create).RequireAuthorization();
        router.MapPut("/{id:guid}", couponsController.Update).RequireAuthorization();
        router.MapDelete("/{id:guid}", couponsController.Delete).RequireAuthorization();
        router.MapGet("/Search", couponsController.Search).RequireAuthorization();
    }
}



