namespace order_management.auth;

public interface IAccountService
{
    Task<Response> Register(RegisterDTO registerDTO);
    Task<LoginResponse> Login(LoginDTO loginDTO);
}
