using Order_Management.api;

namespace Order_Management.src.api
{
    public static class CustomerRoutes
    {
        public static void MapCustomerRoutes(this WebApplication app)
        {
            app.MapGet("/OrderManagementService/GetAllCustomer", CustomerController.GetAllCustomers).RequireAuthorization();
            app.MapGet("/OrderManagementService/GetCustomer/{id:guid}", CustomerController.GetCustomerById).RequireAuthorization();
            app.MapPost("/OrderManagementService/AddCustomer", CustomerController.AddCustomer).RequireAuthorization();
            app.MapPut("/OrderManagementService/UpdateCustomer/{id:guid}", CustomerController.UpdateCustomer).RequireAuthorization();
            app.MapDelete("/OrderManagementService/DeleteCustomer/{id:guid}", CustomerController.DeleteCustomer).RequireAuthorization();
            app.MapGet("/OrderManagementService/SearchCustomer", CustomerController.SearchCustomers).RequireAuthorization();
        }
    }
}