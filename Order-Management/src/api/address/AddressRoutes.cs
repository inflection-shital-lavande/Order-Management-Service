

using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using order_management.database.dto;
using order_management.database.models;
using order_management.services.interfaces;

namespace order_management.api
{

    public class AddressRoutes
    {


        public void MapAddressRoutes(WebApplication app)//, AddressController addressController)
        {
            //var addressController = new AddressController();
            var router = app.MapGroup("/api/addresses").WithTags("AddressController");

           /* router.MapGet("/", async ([FromServices] AddressController addressController) => 
            {return await addressController.GetAll();
            }).RequireAuthorization();*/
            router.MapGet("/", (HttpContext context,[FromServices] AddressController addressController) => addressController.GetAll(context)).RequireAuthorization();
            router.MapGet("/{id:guid}", ([FromServices] AddressController addressController )=> addressController.GetById).RequireAuthorization();//, Guid id
            router.MapPost("/", ([FromServices] AddressController addressController , [FromBody] AddressCreateModel addr) => addressController.Create(addr)).RequireAuthorization();
            router.MapPut("/{id:guid}", ([FromServices] AddressController addressController, Guid id, [FromBody] AddressUpdateModel addr) => addressController.Update(id,addr)).RequireAuthorization();
            router.MapDelete("/{id:guid}", ([FromServices] AddressController addressController, Guid id)=> addressController.Delete(id)).RequireAuthorization();
          // router.MapGet("/search", (HttpContext httpContext,[FromServices] AddressController addressController)=> addressController.Search(httpContext)).RequireAuthorization();

            router.MapGet("/search", async ([FromServices] AddressController addressController, [FromQuery] string? AddressLine1, [FromQuery] string? AddressLine2, [FromQuery] string? City, [FromQuery] string? State, [FromQuery] string? Country, [FromQuery] string? ZipCode) =>
            {
                return await addressController.Search(AddressLine1, AddressLine2, City, State, Country, ZipCode);
            }).RequireAuthorization();

        }
    }
}
    //}





/*

using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using order_management.database.dto;
using order_management.services.interfaces;

namespace order_management.api
{
    public class AddressRoutes
    {
        private readonly AddressController _addressController;


        public AddressRoutes(AddressController addressController)
        {
            _addressController = addressController;
        }
        public void MapRoutes(WebApplication app)
        {
            
            var router = app.MapGroup("/api/addresses");

            router.MapGet("/", _addressController.GetAll).RequireAuthorization();
            router.MapGet("/{id:guid}", _addressController.GetById).RequireAuthorization();
            router.MapPost("/", _addressController.Create).RequireAuthorization();
            router.MapPut("/{id:guid}", _addressController.Update).RequireAuthorization();
            router.MapDelete("/{id:guid}", _addressController.Delete).RequireAuthorization();
            router.MapGet("/search", _addressController.Search).RequireAuthorization();
        }
    }
}
*/