namespace order_management.auth;

public interface IAccountRepo
{
    Task<Response> Register(RegisterDTO registerDTO);
    Task<LoginResponse> Login(LoginDTO loginDTO);
}
