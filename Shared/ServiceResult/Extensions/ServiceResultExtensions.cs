using System.Net;

namespace Shared.ServiceResult.Extensions;

public static class ServiceResultExtension
{
    public static IResult ToResult<T>(this ServiceResult<T> serviceResult)
    {

        return serviceResult.StatusCode switch
        {
            HttpStatusCode.OK => Results.Ok(serviceResult.Data),

            HttpStatusCode.Created => Results.Created($"/api/resource/{serviceResult.Data}", serviceResult.Data),

            HttpStatusCode.BadRequest => Results.BadRequest(serviceResult.Fail),

            HttpStatusCode.NotFound => Results.NotFound(serviceResult.Fail),

            HttpStatusCode.Conflict => Results.Conflict(serviceResult),

            _ => Results.Json(serviceResult, statusCode: (int)serviceResult.StatusCode)

        };

    }

    public static IResult ToResult(this ServiceResult serviceResult)
    {
        return serviceResult.StatusCode switch
        {
            HttpStatusCode.NoContent=>Results.NoContent(),
            
            HttpStatusCode.BadRequest => Results.BadRequest(serviceResult.Fail),

            HttpStatusCode.NotFound => Results.NotFound(serviceResult.Fail),

            HttpStatusCode.Conflict => Results.Conflict(serviceResult.Fail),

            _ => Results.Json(serviceResult, statusCode: (int)serviceResult.StatusCode)
        };
        
    }
}
