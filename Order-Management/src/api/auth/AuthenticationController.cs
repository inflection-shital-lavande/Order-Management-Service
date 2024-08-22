

using FluentValidation;
using Microsoft.Win32;
using order_management.auth;
using order_management.common;
using order_management.database.dto;
using order_management.services.interfaces;
using System.ComponentModel.DataAnnotations;

namespace order_management.api;

public  class AuthenticationController
{


    public  async Task<IResult> Register(RegisterDTO register, IValidator<RegisterDTO> validator, IAccountService accountService)
    {
        try
        {
            var validationResult = validator.Validate(register);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }
            var validationContext = new ValidationContext(register);
            var vResult = new List<ValidationResult>();

            var isvalid = Validator.TryValidateObject(register, validationContext, vResult, true);

            if (isvalid)
            {
                var result = await accountService.Register(register);
                return ApiResponse.Success("Success", "Register successfully", result);
            }
            return Results.BadRequest(vResult);
        }
        catch (Exception ex)
        {
            return Results.Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    public  async Task<IResult> Login(LoginDTO login, IValidator<LoginDTO> validator, IAccountService accountService)
    {
        try
        {
            var validationResult = validator.Validate(login);
            if (!validationResult.IsValid)
            {
                return ApiResponse.BadRequest("Failure", validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var validationContext = new ValidationContext(login);
            var vResult = new List<ValidationResult>();

            var isvalid = Validator.TryValidateObject(login, validationContext, vResult, true);

            if (isvalid)
            {
                var result = await accountService.Login(login);
                return Results.Ok(result);
            }
            return Results.BadRequest(vResult);
        }
        catch (Exception ex)
        {
            return Results.Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}



