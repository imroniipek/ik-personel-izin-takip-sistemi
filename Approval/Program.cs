using Approval.Approval.Application;
using Approval.Approval.Application.Abstraction.Client;
using Approval.Approval.Application.Abstraction.Repository;
using Approval.Approval.Infrastucture.Context;
using Approval.Approval.Infrastucture.Repository;
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
            .WithOrigins("http://localhost:4200", "https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<ApprovalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddScoped<IApprovalRepository, ApprovalRepository>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(
        typeof(Approval.Approval.Application.Features.CreateNewApproval.CreateApproval).Assembly
    ));

builder.Services.AddRefitClient<IGetPersonelByManagerIdFromServices>()
    .ConfigureHttpClient(c =>
        c.BaseAddress = new Uri(builder.Configuration["Services:PersonelServiceUrl"]!));

builder.Services.AddRefitClient<IGetLeaveListForApproval>()
    .ConfigureHttpClient(c =>
        c.BaseAddress = new Uri(builder.Configuration["Services:LeaveServiceUrl"]!));

builder.Services.AddRefitClient<IPutLeaveAfterApproval>()
    .ConfigureHttpClient(c =>
        c.BaseAddress = new Uri(builder.Configuration["Services:LeaveServiceUrl"]!));

builder.Services.AddRefitClient<IGetPendingListByPersonelId>()
    .ConfigureHttpClient(c =>
        c.BaseAddress = new Uri(builder.Configuration["Services:LeaveServiceUrl"]!));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.AddAllExtension();

app.Run();