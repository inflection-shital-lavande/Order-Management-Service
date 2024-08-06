using Order_Management.api;

namespace Order_Management.src.api
{
    public static class CouponsRoutes
    {
        public static void MapCouponsRoutes(this WebApplication app)
        {
            app.MapGet("/OrderManagementService/GetAllCoupon", CouponsController.GetAllCoupons).RequireAuthorization();
            app.MapGet("/OrderManagementService/GetCoupon/{id:guid}", CouponsController.GetCouponById).RequireAuthorization();
            app.MapPost("/OrderManagementService/AddCoupon", CouponsController.AddCoupon).RequireAuthorization();
            app.MapPut("/OrderManagementService/UpdateCoupon/{id:guid}", CouponsController.UpdateCoupon).RequireAuthorization();
            app.MapDelete("/OrderManagementService/DeleteCoupon/{id:guid}", CouponsController.DeleteCoupon).RequireAuthorization();
            app.MapGet("/OrderManagementService/SearchCoupon", CouponsController.SearchCoupons).RequireAuthorization();
        }
    }
}