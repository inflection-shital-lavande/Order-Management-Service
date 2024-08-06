using Microsoft.Win32;
using OrderManagementService.api;

namespace Order_Management.src.api
{
    public static class AuthenticationRoutes
    {
        public static void MapAuthRoutes(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("/register", AuthenticationController.Register)
               .AllowAnonymous();

            routes.MapPost("/login", AuthenticationController.Login)
               .AllowAnonymous();
        }
    }
}

