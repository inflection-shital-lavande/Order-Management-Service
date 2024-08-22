using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using order_management.database.dto;
using order_management.services.interfaces;
using System.ComponentModel.DataAnnotations;

namespace order_management.api;

public class CouponsController
{


    public CouponsController()
    {

    }
    public  async Task<IResult> GetAll(HttpContext httpContext, ICouponService _couponService)
    {
        try
        {

            var coupons = await _couponService.GetAll();
            return ApiResponse.Success("Success", "Coupons retrieved successfully", coupons);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex,"Failure", "An error occurred while retrieving coupons");
        }
    }



    public  async Task<IResult> GetById(ICouponService _couponService, Guid id, HttpContext httpContext)
    {
        try
        {
            var coupon = await _couponService.GetById(id);
            return coupon == null ? ApiResponse.NotFound("Failure", "Coupon not found") 
                                  : ApiResponse.Success("Success", "Coupon retrieved successfully", coupon);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving coupons");
        }
    }

    public async Task<IResult> Create(CouponCreateModel couponCreate, HttpContext httpContext, ICouponService _couponService, IValidator<CouponCreateModel> _createValidator)
    {
        try
        {
            if (couponCreate == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid coupon data");
            }

            var validationResult = _createValidator.Validate(couponCreate);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var createdAddress = await _couponService.Create(couponCreate);
            return ApiResponse.Success("Success", "Coupon created successfully", createdAddress);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while creating the address");
        }
    }

    public  async Task<IResult> Update(ICouponService _couponService, Guid id, IValidator<CouponUpdateModel> _updateValidator, CouponUpdateModel couponUpdate)
    {
        try
        {
            if (couponUpdate == null)
            {
                return ApiResponse.BadRequest("Failure", "Invalid coupon data");
            }

            var validationResult = _updateValidator.Validate(couponUpdate);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var validationContext = new ValidationContext(couponUpdate);
            var vResult = new List<ValidationResult>();

            var isvalid = Validator.TryValidateObject(couponUpdate, validationContext, vResult, true);

            if (isvalid)
            {
                var updatedCoupon = await _couponService.Update(id, couponUpdate);
                return updatedCoupon == null ? ApiResponse.NotFound("Failure", "Coupon not found") 
                                             : ApiResponse.Success("Success", "Coupon updated successfully");
            }
            return Results.BadRequest(vResult);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving coupons");
        }
    }

    public  async Task<IResult> Delete(ICouponService _couponService, Guid id, HttpContext httpContext)
    {
        try
        {
            var deleteResult = await _couponService.Delete(id);
            return deleteResult
                         ? ApiResponse.Success("Success", "Coupon deleted successfully", deleteResult)
                         :ApiResponse.NotFound("Failure", "Coupon not found");
                                        

        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving coupons");
        }
    }

    

    public async Task<IResult> Search(ICouponService _couponService, string? name, string? couponCode)
    {
        try
        {
            
            var filter = new CouponSearchFilterModel
            {
                Name = name,
                CouponCode = couponCode
            };

            var coupons = await _couponService.Search(filter);

            
            return coupons.Items.Any()
                ? ApiResponse.Success("Success", "Coupons retrieved successfully with filters", coupons)
                : ApiResponse.NotFound("Failure", "No coupons found matching the filters");
        }
        catch (Exception ex)
        {
            
            return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving coupons");
        }
    }

}



