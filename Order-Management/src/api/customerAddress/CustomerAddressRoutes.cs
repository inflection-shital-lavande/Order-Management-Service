using Microsoft.AspNetCore.Builder;
using order_management.api;

namespace Order_Management.src.api.customerAddress
{
    public class CustomerAddressRoutes
    {
        public void MapCARoutes(WebApplication app)
        {
            var CustomerAddressController = new CustomerAddressController();
            var router = app.MapGroup("/api/ca");

            router.MapGet("/", CustomerAddressController.GetAll).RequireAuthorization();
            router.MapPost("/", CustomerAddressController.Create).RequireAuthorization();
        }
    }
}
