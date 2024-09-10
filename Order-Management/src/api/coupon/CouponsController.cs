using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using order_management.database.dto;
using order_management.database.models;
using order_management.domain_types.enums;
using order_management.services.interfaces;
using Order_Management.src.database.dto.cart;
using Order_Management.src.services.interfaces;
using System.ComponentModel.DataAnnotations;

namespace order_management.api;

public class CouponsController
{


    private readonly ICouponService _couponService;
    private readonly IValidator<CouponCreateModel> _createValidator;
    private readonly IValidator<CouponUpdateModel> _updateValidator;
    public CouponsController(ICouponService couponService,
                            IValidator<CouponCreateModel> createValidator,
                            IValidator<CouponUpdateModel> updateValidator)
    {
        _couponService = couponService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [ProducesResponseType(200, Type = typeof(IEnumerable<Coupon>))]

    public async Task<IResult> GetAll(HttpContext httpContext)//, ICouponService _couponService)
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



    public  async Task<IResult> GetById( Guid id)// ICouponService _couponService  httpContext)
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

    public async Task<IResult> Create(CouponCreateModel couponCreate)//, HttpContext httpContext)//, ICouponService _couponService, IValidator<CouponCreateModel> _createValidator)
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

    

    public async Task<IResult> Update(Guid id, CouponUpdateModel couponUpdate)//, HttpContext httpContext, ICouponService _couponService, IValidator<CouponUpdateModel> _updateValidator)
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

            var updatedCoupon = await _couponService.Update(id, couponUpdate);
            return updatedCoupon == null ? ApiResponse.NotFound("Failure", "Coupon not found")
                                          : ApiResponse.Success("Success", "Coupon updated successfully", updatedCoupon);
        }
        catch (Exception ex)
        {
            return ApiResponse.Exception(ex, "Failure", "An error occurred while updating the coupons");
        }
    }



    public  async Task<IResult> Delete( Guid id ) // ICouponService _couponService,  HttpContext httpContext)
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

    

    public async Task<IResult> Search( [FromQuery] string? name,
                                        [FromQuery] string? couponCode,
                                        [FromQuery] DateTime? startDate,
                                         [FromQuery] float? discount,
                                          [FromQuery] DiscountTypes? discountType,
                                          [FromQuery] float? discountPercentage,
                                          [FromQuery] float? minOrderAmount,
                                          [FromQuery] bool? isActive)//ICouponService _couponService,
    {
        try
        {
            
            var filter = new CouponSearchFilterModel
            {
                Name = name,
                CouponCode = couponCode,
                StartDate = startDate,
                Discount = discount,
                DiscountType = discountType ?? DiscountTypes.FLAT,
                DiscountPercentage = discountPercentage,
                MinOrderAmount = minOrderAmount,
                IsActive = isActive
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





/*  public  async Task<IResult> Update(ICouponService _couponService, Guid id, IValidator<CouponUpdateModel> _updateValidator, CouponUpdateModel couponUpdate)
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
    }*/