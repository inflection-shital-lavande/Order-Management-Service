

using Order_Management.Auth;

namespace OrderManagementService.app.api.Authen
{
    public static class AuthenticationEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {

            //authentication 

            app.MapPost("/register", async (RegisterDTO register, IAccountService accountService) =>
            {
                return Results.Ok(await accountService.Register(register));

            }).AllowAnonymous();

            app.MapPost("/login", async (LoginDTO login, IAccountService accountService) =>
            {
                return Results.Ok(await accountService.Login(login));

            }).AllowAnonymous();


        }
    }
}