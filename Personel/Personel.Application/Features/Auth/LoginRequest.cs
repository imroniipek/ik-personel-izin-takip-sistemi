namespace Personel.Personel.Application.Features.Auth;

public record LoginRequest(string? UserName,string? Password,string? Email,string? Role);