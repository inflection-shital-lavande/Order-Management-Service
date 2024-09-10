using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using order_management.api;
using order_management.database.dto;
using order_management.domain_types.enums;

namespace order_management.src.api;

public  class CouponsRoutes
{
    public  void MapCouponsRoutes( WebApplication app)
    {
       // var couponsController = new CouponsController();
        var router = app.MapGroup("/api/coupon").WithTags("CouponController");

         router.MapGet("/",(HttpContext httpContext, [FromServices] CouponsController  couponsController) => couponsController.GetAll(httpContext)).RequireAuthorization();
         router.MapGet("/{id:guid}", ( [FromServices] CouponsController couponsController, Guid id) => couponsController.GetById(id)).RequireAuthorization();
         router.MapPost("/", ([FromServices] CouponsController couponsController, CouponCreateModel couponCreate) => couponsController.Create(couponCreate)).RequireAuthorization();
         router.MapPut("/{id:guid}", ([FromServices] CouponsController couponsController, Guid id, CouponUpdateModel couponUpdate) => couponsController.Update(id,couponUpdate)).RequireAuthorization();
         router.MapDelete("/{id:guid}", ([FromServices] CouponsController couponsController, Guid id) => couponsController.Delete(id)).RequireAuthorization();
        router.MapGet("/search", (
           [FromQuery] string? name,
           [FromQuery] string? couponCode,
           [FromQuery] DateTime? startDate,
           [FromQuery] float? discount,
           [FromQuery] DiscountTypes? discountType,
           [FromQuery] float? discountPercentage,
           [FromQuery] float? minOrderAmount,
           [FromQuery] bool? isActive,
           [FromServices] CouponsController couponsController) =>
           couponsController.Search(name, couponCode, startDate, discount, discountType, discountPercentage, minOrderAmount, isActive)
       ).RequireAuthorization();

        //router.MapGet("/Search", ([FromServices] CouponsController couponsController) => couponsController.Search).RequireAuthorization();
    }
}



