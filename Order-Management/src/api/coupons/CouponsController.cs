using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using Order_Management.database.dto;
using Order_Management.services.interfaces;
using System.ComponentModel.DataAnnotations;

namespace Order_Management.api
{
    public static class CouponsController
    {
        public static async Task<IResult> GetAllCoupons(ICouponService couponService)
        {
            try
            {

                var coupons = await couponService.GetAll();
                return ApiResponse.Success("Success", "Coupons retrieved successfully", coupons);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex,"Failure","not found");
            }
        }

        public static async Task<IResult> GetCouponById(ICouponService couponService, Guid id)
        {
            try
            {
                var coupon = await couponService.GetCouponByIdAsync(id);
                return coupon == null ? ApiResponse.NotFound("Failure", "Coupon not found") : ApiResponse.Success("Success", "Coupon retrieved successfully", coupon);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure","not found");
            }
        }

        public static async Task<IResult> AddCoupon(ICouponService couponService, IValidator<CouponCreateDTO> validator , [FromBody] CouponCreateDTO couponCreateDTO)
        {
            try
            {
                if (couponCreateDTO == null)
                {
                    return ApiResponse.BadRequest("Failure", "Invalid coupon data");
                }
                var validationResult = validator.Validate(couponCreateDTO);
                if (!validationResult.IsValid)
                {
                    return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
                }

                var validationContext = new ValidationContext(couponCreateDTO);
                var vResult = new List<ValidationResult>();

                var isvalid = Validator.TryValidateObject(couponCreateDTO, validationContext, vResult, true);

                if (isvalid)
                {
                    var createdCoupon = await couponService.CreateCouponAsync(couponCreateDTO);
                    return ApiResponse.Created("Success", "Coupon created successfully", createdCoupon);
                }
                return Results.BadRequest(vResult);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure","not found");
            }
        }

        public static async Task<IResult> UpdateCoupon(ICouponService couponService, Guid id, IValidator<CouponUpdateDTO> validator, [FromBody] CouponUpdateDTO couponUpdateDTO)
        {
            try
            {
                if (couponUpdateDTO == null)
                {
                    return ApiResponse.BadRequest("Failure", "Invalid coupon data");
                }

                var validationResult = validator.Validate(couponUpdateDTO);
                if (!validationResult.IsValid)
                {
                    return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
                }

                var validationContext = new ValidationContext(couponUpdateDTO);
                var vResult = new List<ValidationResult>();

                var isvalid = Validator.TryValidateObject(couponUpdateDTO, validationContext, vResult, true);

                if (isvalid)
                {
                    var updatedCoupon = await couponService.UpdateCouponAsync(id, couponUpdateDTO);
                    return updatedCoupon == null ? ApiResponse.NotFound("Failure", "Coupon not found") : ApiResponse.Success("Success", "Coupon updated successfully");
                }
                return Results.BadRequest(vResult);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure","not found");
            }
        }

        public static async Task<IResult> DeleteCoupon(ICouponService couponService, Guid id)
        {
            try
            {
                var deleteResult = await couponService.DeleteCouponAsync(id);
                return deleteResult == null ? ApiResponse.NotFound("Failure", "Coupon not found") : ApiResponse.Success("Success", "Coupon deleted successfully", deleteResult);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure","not found");
            }
        }

        public static async Task<IResult> SearchCoupons(ICouponService couponService, [FromQuery] string? name)
        {
            try
            {
                var filterDTO = new CouponSearchFilterDTO { Name = name };
                var coupons = await couponService.SearchCouponAsync(filterDTO);
                return ApiResponse.Success("Success", "Coupons retrieved successfully with filters", coupons);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure","not found");
            }
        }
    }
}
