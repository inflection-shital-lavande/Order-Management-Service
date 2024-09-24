using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace order_management.common;

public record ApiResponse
{


    
        public static IResult Success(string status, string message, object? data = null)
        {
            return Results.Ok(new
            {
                Status= status,                   
                Message = message,
                HttpCode = 200,
                Data = data
            });
        }

        public static IResult Created(string status, string message, object? data = null)
        {
            return Results.Created(string.Empty, new
            {
                Status = status,                   
                Message = message,
                HttpCode = 201,
                Data = data
            });
        }

        public static IResult NotFound(string status, string message, object? data = null)
        {
            return Results.NotFound(new
            {
                Status = status,
                Message = message,
                HttpCode = 404,
                Data = data
            });
    }

    public static IResult BadRequest(string status, IEnumerable<string> messages,object? data = null)
    {
        return Results.BadRequest(new
        {
            Status = status,
            Message = messages,
            HttpCode = 400,
            Data = data

        });
    }

   public static IResult BadRequest(string status, string message, object? data = null)
        {
            return Results.BadRequest(new
            {
                Status = status,
                Message = message,
                HttpCode = 400,                  
                Data = data
            });
        }

   

    public static IResult Conflict(string status, string message, object? data = null)
    {
        return Results.Conflict(new
        {
            Status = status,
            Message = message,
            HttpCode = 409,
            Data = data
        });
    }

    public static IResult Exception(Exception ex, string status, string message )
   {
       return Results.Problem(new
            {
           Status = status,
           Message = message,
           HttpCode = 500,             

         }.ToString());
        }


    




}







