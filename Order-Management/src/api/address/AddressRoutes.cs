

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
            var addressController = new AddressController();
            var router = app.MapGroup("/api/addresses");

           
            router.MapGet("/",  addressController.GetAll).RequireAuthorization();
            router.MapGet("/{id:guid}",  addressController.GetById).RequireAuthorization();//, Guid id ([FromServices] AddressController addressController )=>
            router.MapPost("/",  addressController.Create).RequireAuthorization();
            router.MapPut("/{id:guid}",  addressController.Update).RequireAuthorization();
            router.MapDelete("/{id:guid}",  addressController.Delete).RequireAuthorization();
            router.MapGet("/search", addressController.Search).RequireAuthorization();

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