using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using order_management.database.dto;
using order_management.database.models;
using order_management.services.interfaces;
using Order_Management.src.database.dto.merchant;
using Order_Management.src.services.implementetions;
using Order_Management.src.services.interfaces;

namespace Order_Management.src.api.merchant;


    public class MerchantController
    {
        public MerchantController()
        {

        }

        [ProducesResponseType(200, Type = typeof(IEnumerable<Merchant>))]

        public async Task<IResult> GetAll(HttpContext httpContext, IMerchantService _merchantService)
        {
            try
            {
                var merchant = await _merchantService.GetAll();
                return ApiResponse.Success("Success", "merchant retrieved successfully", merchant);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving merchant");
            }
        }
        public async Task<IResult> GetById(Guid id, HttpContext httpContext, IMerchantService _merchantService)
        {
            try
            {
                var merchant = await _merchantService.GetById(id);
                return merchant == null ? ApiResponse.NotFound("Failure", "merchant not found")
                                       : ApiResponse.Success("Success", "merchant retrieved successfully", merchant);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while retrieving the merchant");
            }
        }
     public async Task<IResult> Create(MerchantCreateModel Create, HttpContext httpContext, IMerchantService _merchantService, IValidator<MerchantCreateModel> _createValidator)
     {
         try
         {
             if (Create == null)
             {
                 return ApiResponse.BadRequest("Failure", "Invalid merchant data");
             }

             var validationResult = _createValidator.Validate(Create);
             if (!validationResult.IsValid)
             {
                 return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
             }

             var merchant = await _merchantService.Create(Create);
             return ApiResponse.Success("Success", "merchant created successfully", merchant);
         }
         catch (Exception ex)
         {
             return ApiResponse.Exception(ex, "Failure", "An error occurred while creating the merchant");
         }
     }
    

  

public async Task<IResult> Update(Guid id, MerchantUpdateModel merchant, HttpContext httpContext, IMerchantService _merchantService, IValidator<MerchantUpdateModel> _updateValidator)
        {
            try
            {
                if (merchant == null)
                {
                    return ApiResponse.BadRequest("Failure", "Invalid merchant data");
                }

                var validationResult = _updateValidator.Validate(merchant);
                if (!validationResult.IsValid)
                {
                    return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
                }

                var updatedMerchant = await _merchantService.Update(id, merchant);
                return updatedMerchant == null ? ApiResponse.NotFound("Failure", "merchant not found")
                                              : ApiResponse.Success("Success", "merchant updated successfully", updatedMerchant);
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while updating the merchant");
            }
        }
        public async Task<IResult> Delete(Guid id, HttpContext httpContext, IMerchantService _merchantService)
        {
            try
            {
                var success = await _merchantService.Delete(id);
                return success ? ApiResponse.Success("Success", "merchant deleted successfully")
                               : ApiResponse.NotFound("Failure", "merchant not found");
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while deleting the merchant");
            }
        }

        public async Task<IResult> Search([FromQuery] string? name,
                                          [FromQuery] string? email,
                                          [FromQuery] string? phone,
                                          [FromQuery] string? taxNumber,
                                          [FromQuery] DateTime? createdBefore,
                                          [FromQuery] DateTime? createdAfter,
                                          [FromQuery] int? pastMonths,
                                           HttpContext httpContext, IMerchantService _merchantService)
        {
            try
            {
                var filter = new MerchantSearchFilter
                {
                    Name = name,
                    Email = email,
                    Phone = phone,
                    TaxNumber = taxNumber,
                    CreatedBefore = createdBefore,
                    CreatedAfter = createdAfter,
                    PastMonths = pastMonths
                };

                var merchant = await _merchantService.Search(filter);
                return merchant.Items.Any()
                    ? ApiResponse.Success("Success", "merchant retrieved successfully with filters", merchant)
                    : ApiResponse.NotFound("Failure", "No merchant found matching the filters");
            }
            catch (Exception ex)
            {
                return ApiResponse.Exception(ex, "Failure", "An error occurred while searching for merchant");
            }
        }
    }




