﻿namespace Api.DTOs.Requests
{
    public record LoginRequest(
        string Email, 
        string Password
    );
}
