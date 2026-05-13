
using Personel.Personel.Application.Features.Auth;
using Personel.Personel.Application.Features.Department.CreateDepartment;
using Personel.Personel.Application.Features.Department.GetAllDepartmentWithNames;
using Personel.Personel.Application.Features.Department.GetDepartmentCount;
using Personel.Personel.Application.Features.Personel.GetPersonelsByManagerId;
using Personel.Personel.Application.Features.Personel.CreateManager;
using Personel.Personel.Application.Features.Personel.CreatePersonel;
using Personel.Personel.Application.Features.Personel.DeleteTheManager;
using Personel.Personel.Application.Features.Personel.DeleteThePersonel;
using Personel.Personel.Application.Features.Personel.GetAllPersonel;
using Personel.Personel.Application.Features.Personel.GetAllPersonelsByDepartmentId;
using Personel.Personel.Application.Features.Personel.GetManagerByEmail;
using Personel.Personel.Application.Features.Personel.GetManagersCount;
using Personel.Personel.Application.Features.Personel.GetPersonelByEmail;
using Personel.Personel.Application.Features.Personel.GetPersonelByPersonelId;
using Personel.Personel.Application.Features.Personel.GetPersonelForLeave;
using Personel.Personel.Application.Features.Personel.GetPersonelsCount;
using Personel.Personel.Application.Features.Personel.GetThePersonel;
using Personel.Personel.Application.Features.Personel.UpdateTheManager;
using Personel.Personel.Application.Features.Personel.UpdateThePersonel;

namespace Personel.Personel.Application.Extension;

public static class EndpointExtensions
{
    public static WebApplication MapPersonelEndpoints(this WebApplication app)
    {
        var authGroup = app.MapGroup("/api/auth");
        authGroup.AddAuthEndpoint();

        var internalGroup = app.MapGroup("/api");

        var adminGroup = app.MapGroup("/api")
            .RequireAuthorization("AdminOnly");

        var personelGroup = app.MapGroup("/api")
            .RequireAuthorization();

        adminGroup.CreateNewDepartment();
        adminGroup.CreateNewPersonelEndpoint();
        adminGroup.AddNewManagerByGivenDepartmentId();
        adminGroup.AddDeleteTheManagerEndpoint();
        adminGroup.AddDeleteThePersonelCommandEndpoint();
        adminGroup.AddUpdateThePersonelCommandEndpoint();
        adminGroup.AddUpdateTheManagerEndpoint();
        adminGroup.AddGetAllPersonelQueryEndpoint();
        adminGroup.MapGetAllPersonelCountEndpoint();
        adminGroup.AddGetPersonelIdEndpoint();
        adminGroup.GetTheDepartmentCount();
        adminGroup.AddGetAllDepartmentsWithNamesQueryEndpoint();
        adminGroup.AddGetAllPersonelByDepartmentIdEnpoint();
        adminGroup.AddGetManagerCountManager();
        internalGroup.MapGetPersonelForLeaveEndpoint();
        internalGroup.GetPersonelsByManagerIdRoute();
        personelGroup.AddGetPersonelByEmailEndpoint();
        personelGroup.AddGetThePersonelQueryEndpoint();
        personelGroup.AddGetManagerByEmailEndpoint();
        return app;
    }
}