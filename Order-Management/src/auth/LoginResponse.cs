namespace order_management.auth;

public record LoginResponse(bool Success, string Token = null!, string Message = null!);
