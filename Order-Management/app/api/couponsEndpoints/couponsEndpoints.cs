using Microsoft.AspNetCore.Mvc;
using Order_Management.app.database.service;
using Order_Management.app.domain_types.dto;
using Order_Management.app.domain_types.dto.couponDTO;

namespace Order_Management.app.api.couponsEndpoints
{
    public static class couponsEndpoints
    {

        public static void MapCouponsEndpoints(this WebApplication app)
        {
            app.MapGet("/OrderManagementService/GetAllCoupon", async (ICouponService couponService) =>
            {

                var coupons = await couponService.GetAll();
                return Results.Ok(new
                {
                    Message = "Addresses retrieved successfully",
                    Data = coupons
                });

            }).RequireAuthorization();

            app.MapGet("/OrderManagementService/GetCoupon/{id:guid}", async (ICouponService couponService, Guid id) =>
            {
                var coupons = await couponService.GetCouponByIdAsync(id);
                if (coupons == null)
                {
                    return Results.NotFound(new { Message = "Address not found" });
                }

                return Results.Ok(new
                {
                    Message = "Address retrieved successfully",
                    Data = coupons
                });
            }).RequireAuthorization();

            app.MapPost("/OrderManagementService/AddCoupon", async (ICouponService couponService, couponCreateDTO couponCreateDTO) =>
            {
                var createdCoupon = await couponService.CreateCouponAsync(couponCreateDTO);

                return Results.Created($"/OrderManagementService/Coupon/{createdCoupon}", new
                {
                    Message = "Address created successfully",
                    Data = createdCoupon
                });
            }).RequireAuthorization();

            app.MapPut("/OrderManagementService/UpdateCoupon/{id:guid}", async (ICouponService couponService, Guid id, couponUpdateDTO couponUpdateDTO) =>
            {
                var updatedCoupon = await couponService.UpdateCouponAsync(id, couponUpdateDTO);
                if (updatedCoupon == null)
                {
                    return Results.NotFound(new { Message = "Address not found" });
                }

                return Results.Ok(new { Message = "Address updated successfully" });
            }).RequireAuthorization();

            app.MapDelete("/OrderManagementService/DeleteCoupon/{id:guid}", async (ICouponService couponService, Guid id) =>
            {

                var Coupon = await couponService.DeleteCouponAsync(id);
                return Results.Ok(new
                {
                    Message = "Addresses deleted successfully",
                    Data = Coupon
                });
            }).RequireAuthorization();

            app.MapGet("/OrderManagementService/SearchCoupon", async (ICouponService couponService,
                                                                       [FromQuery] string? name
                                                                        ) =>
            {
                var filterDTO = new couponSearchFilterDTO
                {
                    Name = name,
                   
                };

                var Coupon = await couponService.SearchCouponAsync(filterDTO);

                return Results.Ok(new
                {
                    Message = "Addresses retrieved successfully with filters",
                    Data = Coupon
                });
            }).RequireAuthorization();
        }
    }
}

