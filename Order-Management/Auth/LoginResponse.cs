﻿namespace Order_Management.Auth
{
    public record LoginResponse(bool Flag, string Token = null!, string Message = null!);
}
