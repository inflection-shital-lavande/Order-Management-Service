using Microsoft.AspNetCore.Builder;

namespace Order_Management.src.api.payment_transaction
{
    public class Payment_Transaction_Routes
    {
        public void MapPaymentTransectionRoutes(WebApplication app)
        {
            var payment_Transaction_Controller = new Payment_Transaction_Controller();
            var router = app.MapGroup("/api/payment_transections");

            router.MapGet("/", payment_Transaction_Controller.GetAll).RequireAuthorization();
            router.MapGet("/{id:guid}", payment_Transaction_Controller.GetById).RequireAuthorization();
            router.MapPost("/", payment_Transaction_Controller.Create).RequireAuthorization();
            router.MapDelete("/{id:guid}", payment_Transaction_Controller.Delete).RequireAuthorization();
            router.MapGet("/search", payment_Transaction_Controller.Search).RequireAuthorization();
        }
    }
}
