namespace Personel.Personel.Application.Features.Auth;

public record LoginResponse(string Token,string Role,int ?PersonelId,string ? Email);