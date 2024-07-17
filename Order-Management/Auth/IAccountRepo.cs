namespace Order_Management.Auth
{
    public interface IAccountRepo
    {
        Task<Response> Register(RegisterDTO registerDTO);
        Task<LoginResponse> Login(LoginDTO loginDTO);
    }
}
