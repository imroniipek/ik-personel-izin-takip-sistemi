using Leaves.Leave.Infrastucture.Context;
using Leaves.Leave.Infrastucture.Repository;
using Leaves.Leaves.Application.Abstraction.Clients;
using Leaves.Leaves.Application.Abstraction.Repositories;
using Leaves.Leaves.Application.Features.CreateLeave;
using Leaves.Leaves.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<LeaveDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServer")
    );
});

builder.Services.AddScoped<ILeaveRepository, LeaveRepository>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<CreateLeaveCommandHandler>();
});

builder.Services.AddRefitClient<IPersonelApi>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri(
            builder.Configuration["Services:PersonelServiceUrl"] ?? string.Empty
        );
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");

app.LeaveEndpoints();

app.Run();