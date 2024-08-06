using Microsoft.AspNetCore.Builder;
using Order_Management.api;

namespace Order_Management.src.api
{
    public class AddressRoutes
    {
        public void MapAddressRoutes(WebApplication app)
        {
            app.MapGet("/api/", AddressController.GetAllAddresses).RequireAuthorization();
            app.MapGet("/api/{id:guid}", AddressController.GetAddressById).RequireAuthorization();
            app.MapPost("/api/", AddressController.AddAddress).RequireAuthorization();
            app.MapPut("/api/{id:guid}", AddressController.UpdateAddress).RequireAuthorization();
            app.MapDelete("/api/{id:guid}", AddressController.DeleteAddress).RequireAuthorization();
            app.MapGet("/api/Search", AddressController.SearchAddresses).RequireAuthorization();
            //app.MapGet("/Address/{customer:guid}", AddressController.GetCustomerByAddress).RequireAuthorization();
        }
    }
}
