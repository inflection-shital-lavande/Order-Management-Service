using Microsoft.Win32;
using order_management.api;

namespace order_management.src.api;

public  class AuthenticationRoutes
{
    public  void MapAuthRoutes( WebApplication app)
    {
        var authenticationController = new AuthenticationController();
        var router = app.MapGroup("/api/auth");

        router.MapPost("/register", authenticationController.Register)
           .AllowAnonymous();

        router.MapPost("/login", authenticationController.Login)
           .AllowAnonymous();
    }
}

