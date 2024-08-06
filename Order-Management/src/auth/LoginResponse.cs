namespace Order_Management.Auth
{
    public record LoginResponse(bool Success, string Token = null!, string Message = null!);
}
