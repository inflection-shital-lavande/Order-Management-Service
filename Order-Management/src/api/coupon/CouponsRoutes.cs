using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using order_management.api;
using order_management.database.dto;
using order_management.domain_types.enums;
using order_management.services.interfaces;

namespace order_management.src.api;

public  class CouponsRoutes
{
    public  void MapCouponsRoutes( WebApplication app)
    {
        var couponsController = new CouponsController();
        var router = app.MapGroup("/api/coupon");//.WithTags("CouponController");

         router.MapGet("/" ,couponsController.GetAll).RequireAuthorization();
         router.MapGet("/{id:guid}", couponsController.GetById).RequireAuthorization();
         router.MapPost("/", couponsController.Create).RequireAuthorization();
         router.MapPut("/{id:guid}",  couponsController.Update).RequireAuthorization();
         router.MapDelete("/{id:guid}",  couponsController.Delete).RequireAuthorization();
        router.MapGet("/search", couponsController.Search).RequireAuthorization();

        //router.MapGet("/Search", ([FromServices] CouponsController couponsController) => couponsController.Search).RequireAuthorization();
    }
}



