using Personel.Personel.Application.Abstraction;

namespace Personel.Personel.Application.Features.Auth;

public static class AuthEndpoint
{
    public static RouteGroupBuilder AddAuthEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapPost("/login", async (LoginRequest request, IPersonelRepository personelRepository, IDepartmentRepository departmentRepository, TokenService tokenService) =>
        {
            if (request.Role == "admin")
            {
                if (request.UserName != "admin" || request.Password != "12345")
                {
                    return Results.Unauthorized();
                }

                var token = tokenService.CreateToken(
                    personelId: null,
                    email: "admin@system.com",
                    role: "admin",
                    departmentId: null
                );

                return Results.Ok(new LoginResponse(token, "admin", null, "admin@system.com"));
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return Results.BadRequest("Email boş olamaz.");
            }

            var personel = await personelRepository.GetPersonelByEmailAsync(request.Email);

            if (personel is null)
            {
                return Results.NotFound("Personel bulunamadı.");
            }

            if (request.Role == "manager")
            {
                var isManager = await departmentRepository.IsThisAManager(personel.Id);

                if (!isManager)
                {
                    return Results.NotFound("Bu email ile kayıtlı manager bulunamadı.");
                }

                var token = tokenService.CreateToken(
                    personel.Id,
                    personel.Email,
                    "manager",
                    personel.DepartmentId
                );

                return Results.Ok(new LoginResponse(
                    token,
                    "manager",
                    personel.Id,
                    personel.Email
                ));
            }

            if (request.Role == "personel")
            {
                var token = tokenService.CreateToken(
                    personel.Id,
                    personel.Email,
                    "personel",
                    personel.DepartmentId
                );

                return Results.Ok(new LoginResponse(
                    token,
                    "personel",
                    personel.Id,
                    personel.Email
                ));
            }

            return Results.BadRequest("Geçersiz rol.");
        })
        .AllowAnonymous(); //Login Services kısmına herkes girebilir cunku hebuz token yok 

        return builder;
    }
}