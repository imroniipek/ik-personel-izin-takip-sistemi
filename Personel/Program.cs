using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Personel.Personel.Application.Abstraction;
using Personel.Personel.Application.Extension;
using Personel.Personel.Application.Features.Auth;
using Personel.Personel.Application.Features.Department.CreateDepartment;
using Personel.Personel.Infrastucture.Context;
using Personel.Personel.Infrastucture.Repository;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Database
builder.Services.AddDbContext<PersonelDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServer"));
});

// Repository DI
builder.Services.AddScoped<IPersonelRepository, PersonelRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

// Token Service DI
builder.Services.AddScoped<TokenService>();

// MediatR
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(
        typeof(CreateDepartmentCommandHandler).Assembly
    );
});

// Authentication - JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

// Authorization - Role Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("admin"));

    options.AddPolicy("ManagerOnly", policy =>
        policy.RequireRole("manager"));

    options.AddPolicy("PersonelOnly", policy =>
        policy.RequireRole("personel"));

    options.AddPolicy("AdminOrManager", policy =>
        policy.RequireRole("admin", "manager"));
});

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware sırası önemli
app.UseCors("AllowAngular");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

// Endpoints
app.MapPersonelEndpoints();

app.Run();